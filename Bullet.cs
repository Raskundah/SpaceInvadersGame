﻿using System;
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
        public int radius = 18;
        public Vector2 bulletPosition = new Vector2();
        private bool collided = false;

        public Bullet(Vector2 newPos)
        {
            bulletPosition = newPos;
        }

        public void BulletUpdate(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            bulletPosition.Y -= bulletSpeed * dt;  
        }

        public bool Collided
        {
            get { return collided; }
            set { collided = value; }
        }
    }
}
