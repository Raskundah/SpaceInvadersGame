using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;


namespace SpaceInvadersGame
{
    class Player

    {
        // fields

        private GraphicsDeviceManager _graphics;
        public Vector2 playerPosition = new Vector2(0, 0);
        private int speed = 300;
        private KeyboardState kStateOld = Keyboard.GetState();
        private double timer = 0d;
        private double maxTimer = 1d;
        private bool canShoot = true;

        public Player(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
            playerPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight * 0.9f); // janky code/constructor to make graphics calls work in the class
        }

        public void PlayerUpdate(GameTime gameTime)
        {
            KeyboardState kstate = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds; // using delta time to manage consistent speed.

            // movement code for player

            if ( kstate.IsKeyDown(Keys.A)||kstate.IsKeyDown(Keys.Left))
            {
                playerPosition.X -= speed * dt;

                

                if (playerPosition.X <= 0)
                {
                    playerPosition.Y = 1;
                }
                
            }

            if (kstate.IsKeyDown(Keys.D) || kstate.IsKeyDown(Keys.Right))
            {
                playerPosition.X += speed * dt;


                if (playerPosition.X >= _graphics.PreferredBackBufferWidth - 10)
                {
                    playerPosition.Y = _graphics.PreferredBackBufferWidth - 10;
                }

            }


            // handles shooting

            if (kstate.IsKeyDown(Keys.Space) && kStateOld.IsKeyUp(Keys.Space) && (canShoot))
            {
                Bullet.bullets.Add(new Bullet(playerPosition));
                timer = maxTimer;


            }

            kStateOld = kstate;

            // shoot timer to limit projectile spam.

            if (timer > 0)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                canShoot = false;

            }

            else
            {
                
                canShoot = true;
            }
        }
        
    }
}
