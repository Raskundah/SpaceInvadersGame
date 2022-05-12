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
        public float speed = 3000.0f;
        public int radius = 61;
        private bool dead = false;
        Random alienShooting = new Random();
        public int alienNumber;
        public bool subsequentSpawn = true;
        private int speedIncrease = 0;
        private int wavecount = 1;


        public Alien() // pseudo random constructor to choose a random alien.
        {
            alienNumber = alienShooting.Next(0, aliens.Count);
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

        public void SpawnAlien()
        {
            // define starting alien positions.

            wavecount++;

            int startingPosition = 50;
            int yPos = 50;

            // alien spawn loop. to be modified to allow for respawning of new aliens.

            for (int i = 0; i < 12; i++)
            {
                Alien newAlien = new Alien();

                newAlien.position.X = startingPosition;
                newAlien.position.Y = yPos;

                Alien.aliens.Add(newAlien);

                startingPosition += 150;

                if (i == 5)
                {
                    yPos += 100;
                    startingPosition = 50;

                }


                if (wavecount >= 2)
                {
                    speedIncrease += 1000;
                    speed += speedIncrease;
                }
            }
        }
      
        public bool Dead // death logic properties.
        {
            get { return dead; }
            set { dead = value; }
        }

    }
}
