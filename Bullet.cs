using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;



namespace SpaceInvadersGame
{
    class Bullet
    {

        public static List<Bullet> bullets = new List<Bullet>();

        public int bulletSpeed = 150;
        public bool isFired = false;
        public Vector2 bulletPosition = new Vector2();

        public Bullet(Vector2 newPos)
        {
            bulletPosition = newPos;
        }

        public void BulletUpdate(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            bulletPosition.Y -= bulletSpeed * dt;

            
        }




    }
}
