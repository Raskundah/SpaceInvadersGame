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
        public Vector2 playerPosition = new Vector2();       

        public Player(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
            playerPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight - 50);
        }

        public void PlayerUpdate(GameTime gameTime)
        {
            KeyboardState kstate = Keyboard.GetState();


        }



         
    }
}
