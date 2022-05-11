using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvadersGame
{
    class Alien
    {
        public static List<Alien> aliens = new List<Alien>();


        // fields

        public Vector2 position = new Vector2(0, 0);
        public float speed = 200.0f;
        public int radius = 61;
        private bool dead = false;
        Random alienShooting = new Random();
        int number;

        public Alien(int maxAliens) // pseudo random constructor to choose a random alien.
        {
            number = alienShooting.Next(1, maxAliens);
        }


        public bool Update(GameTime gameTime, int textWidth) // update function.
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X += speed * dt;

            if (position.X >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - textWidth || position.X <= 0)
            {
                return true;
            }

            return false;
        }

        public bool Dead // death logic properties.
        {
            get { return dead; }
            set { dead = value; }
        }

    }
}
