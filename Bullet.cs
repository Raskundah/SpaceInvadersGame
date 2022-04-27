using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace SpaceInvadersGame
{
    class Bullet
    {

        public int bulletSpeed = 30;
        public bool isFired = false;
        public Vector2 bulletPosition = new Vector2();

        public void BulletUpdate(GameTime gameTime)
        {

            KeyboardState kstate = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds; // using delta time to manage consistent speed.

            if (kstate.IsKeyDown(Keys.Space) || isFired == true)
            {
                isFired = true;
               // bulletPosition.Y -= bulletSpeed * dt

            }

        }

    }
}
