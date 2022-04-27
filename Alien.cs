using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvadersGame
{
    class Alien
    {
       public Vector2 position = new Vector2(0, 0);
        public float speed = 150.0f; 

        public bool Update(GameTime gameTime, int textWidth)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X += speed * dt;           

            if(position.X >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - textWidth || position.X <= 0)
            {
                // speed += 1.0f;
                return true;
            }

            return false;
        }

    }
}
