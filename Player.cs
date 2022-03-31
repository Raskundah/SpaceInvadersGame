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
        private Vector2 playerPosition = new Vector2();
        private int speed = 3;

        public Player(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
            playerPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight - 50);
        }

        public void PlayerUpdate(GameTime gameTime)
        {
            KeyboardState kstate = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds; // using delta time to manage consistent speed.

            if ( kstate.IsKeyDown(Keys.A)||kstate.IsKeyDown(Keys.Left))
            {
                playerPosition.X -= speed * dt;
            }

            if (kstate.IsKeyDown(Keys.D) || kstate.IsKeyDown(Keys.Right))
            {
                playerPosition.X += speed * dt;

            }
        }
        
    }
}
