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

        // define fields for bullet (Player/Enemy)

        public static List<Bullet> bullets = new List<Bullet>();

        public int bulletSpeed = 500;
        public bool isFired = false;
        public int radius = 18;
        public Vector2 bulletPosition = new Vector2();
        private bool collided = false;

        public Bullet(Vector2 newPos) // Constructor for updating the position from other variables
        {
            bulletPosition = newPos;
        }

        public void BulletUpdate(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds; //handles delta time, the time elapsed between frames

            bulletPosition.Y -= bulletSpeed * dt;  
        }

        public bool Collided // properties of the collided variable for outside use of prviate variable.
        {
            get { return collided; }
            set { collided = value; }
        }
    }
}
