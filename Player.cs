using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvadersGame
{
    class Player

    {
        private GraphicsDeviceManager _graphics;
        public Vector2 playerPosition = new Vector2(0, 0);
        private int speed = 150;

        public Player(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
            playerPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight * 0.9f);
        }

        public void PlayerUpdate(GameTime gameTime)
        {
            KeyboardState kstate = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds; // using delta time to manage consistent speed.

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
        }
        
    }
}
