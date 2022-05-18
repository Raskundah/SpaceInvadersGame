using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Diagnostics;
using System;


namespace SpaceInvadersGame

    // coded by ian barrie
    // 11/05/2022

{
    public static class MySounds
    {
        public static SoundEffect hitSound;
        public static Song mainMusic;
        public static SoundEffect newWave;
        public static Song endMusic;
    }


    public class Game1 : Game
    {
        // define initial variables

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D alienTexture;
        private Texture2D bulletTexture;
        private Texture2D playerTexture;
       // private Texture2D shieldTexture;
        private SpriteFont textFont;
        private int score;
        public int alienValue = 100;
        public bool gameOver = false;
        public float scoreMultiplier = 1.1f;
        Random rand = new Random();
        public int alienNumber;
        public double timer = 2d;
        public double maxTimer = 2d;



        // currently vestigial code for a high score system.

       // private int[] highScore = new int[5] { 0, 0, 0, 0, 0 };


        //create instances of a class.
        Alien alien;
        Player player;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
  
        }
        protected override void Initialize()
        {

            // sets window size to max screen size in windowed mode.
           
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.ApplyChanges();

            alien = new Alien();
            player = new Player(_graphics);

            alien.SpawnAlien();

            base.Initialize();
        }
        protected override void LoadContent()

            //load files from content folder into required addresses
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            alienTexture = Content.Load<Texture2D>("Alien");
            bulletTexture = Content.Load<Texture2D>("Bullet");
            playerTexture = Content.Load<Texture2D>("Ship");
            //shieldTexture = Content.Load<Texture2D>("shield");
            textFont = Content.Load<SpriteFont>("timerFont");

            MySounds.hitSound = Content.Load<SoundEffect>("Sounds/HitSound");
            MySounds.newWave = Content.Load<SoundEffect>("Sounds/WaveClear");
            MySounds.mainMusic = Content.Load<Song>("Sounds/GameLoopAudio");
            MySounds.endMusic = Content.Load<Song>("Sounds/GameOver");

            MediaPlayer.Volume = 0.5f;
            MediaPlayer.Play(MySounds.mainMusic);
        }
        protected override void Update(GameTime gameTime)
        {
           
            
                

            if(gameOver)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(MySounds.endMusic);
            }
            

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            bool hasReached = false;

            // code to check if aliens have reached the side of the screen.

            for (int i = 0; i < Alien.aliens.Count; i++)
            {
                if (Alien.aliens[i].Update(gameTime, alienTexture.Width) && !hasReached)
                {
                    hasReached = true;
                }
            }

            for (int i = 0; i < Alien.bottomAliens.Count; i++)
            {
                if (Alien.bottomAliens[i].Update(gameTime, alienTexture.Width) && !hasReached)
                {
                    hasReached = true;
                }
            }

            if (hasReached)
            {
                foreach (Alien alien in Alien.aliens)
                {
                    alien.position.Y += 50;
                    alien.speed *= -1;
                    alien.Update(gameTime, alienTexture.Width);

                    if (alien.position.Y >= _graphics.PreferredBackBufferHeight * 0.75)
                    {
                        gameOver = true;
                    }
                }

                foreach (Alien alien in Alien.bottomAliens)
                {
                    alien.position.Y += 50;
                    alien.speed *= -1;
                    alien.Update(gameTime, alienTexture.Width);

                    if (alien.position.Y >= _graphics.PreferredBackBufferHeight * 0.75)
                    {
                        gameOver = true;
                    }
                }

            }

            player.PlayerUpdate(gameTime); // calls frame updates for the player 


            foreach (Bullet enemyBullet in Bullet.alienBullets)
            {
                enemyBullet.AlienBulletUpdate(gameTime);
            }

            foreach (Bullet bullets in Bullet.bullets)
            {
                bullets.BulletUpdate(gameTime); //updates each instance of bullet class.
            }

            foreach (Bullet bullets in Bullet.bullets) // collision code.
            {
                foreach (Alien aliens in Alien.aliens)
                {
                    int sum = bullets.radius + aliens.radius;
                    if (Vector2.Distance(bullets.bulletPosition, aliens.position) < sum)
                    {
                        bullets.Collided = true;
                        aliens.Dead = true;
                        score += alienValue;
                        MySounds.hitSound.Play();
                    }

                }

                foreach (Alien aliens in Alien.bottomAliens)
                {
                    int sum = bullets.radius + aliens.radius;
                    if (Vector2.Distance(bullets.bulletPosition, aliens.position) < sum)
                    {
                        bullets.Collided = true;
                        aliens.Dead = true;
                        score += alienValue;

                        MySounds.hitSound.Play();
                    }
                }
            }

            foreach(Bullet bullet in Bullet.alienBullets)
            {
                int newSum = playerTexture.Width/ 4 + bullet.radius;
                if (Vector2.Distance(player.playerPosition - new Vector2(20,20), bullet.bulletPosition) < newSum)
                {
                    bullet.Collided = true;
                    player.lives--;

                    MySounds.hitSound.Play();
                }
            }
 

            Bullet.bullets.RemoveAll(p => p.Collided); // remove collided elements.
            Bullet.alienBullets.RemoveAll(a => a.Collided);
            Alien.aliens.RemoveAll(d => d.Dead);
            Alien.bottomAliens.RemoveAll(d => d.Dead);

            if (Alien.aliens.Count == 0 && Alien.bottomAliens.Count == 0)
            {
                alien.SpawnAlien();
                alienValue = (int)(alienValue * scoreMultiplier);
            }


            if (timer <= 0 && Alien.bottomAliens.Count != 0)
            {
                alienNumber = rand.Next(0, Alien.bottomAliens.Count);
                Bullet alienBullet = new Bullet(Alien.bottomAliens[alienNumber].position);
                Bullet.alienBullets.Add(alienBullet);
                timer = maxTimer;
            }

            else if (timer <= 0 && Alien.aliens.Count != 0)
            {
                alienNumber = rand.Next(0, Alien.aliens.Count);
                Bullet alienBullet = new Bullet(Alien.aliens[alienNumber].position);
                Bullet.alienBullets.Add(alienBullet);
                timer = maxTimer;
            }

            if(player.lives == 0)
            {
                gameOver = true;
            }

            timer -= gameTime.ElapsedGameTime.TotalSeconds;



            base.Update(gameTime);

            
        }
        protected override void Draw(GameTime gameTime)

        {


            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.DrawString(textFont, "Score: " + score, new Vector2(10, 10), Color.White);
             _spriteBatch.DrawString(textFont, "Lives: " + player.lives, new Vector2(10, 50), Color.White);

            if (gameOver == false)
            {

                for (int i = 0; i < Alien.aliens.Count; i++)
                {
                    _spriteBatch.Draw(alienTexture, Alien.aliens[i].position, Color.White);
                }

                for (int i = 0; i < Alien.bottomAliens.Count; i++)
                {
                    _spriteBatch.Draw(alienTexture, Alien.bottomAliens[i].position, Color.White);
                }


                _spriteBatch.Draw(playerTexture, player.playerPosition, Color.White);



                foreach (Bullet bullets in Bullet.bullets)
                {
                    _spriteBatch.Draw(bulletTexture, new Vector2(bullets.bulletPosition.X - bullets.radius, bullets.bulletPosition.Y - bullets.radius * 2), Color.White);
                }

                foreach(Bullet bullets in Bullet.alienBullets)
                {
                    _spriteBatch.Draw(bulletTexture, new Vector2(bullets.bulletPosition.X + bullets.radius, bullets.bulletPosition.Y + bullets.radius * 2), Color.White);

                }



              //  int posModifier = _graphics.PreferredBackBufferWidth / 3 - 175;


             /*   for (int i = 0; i < 3; ++i)
                {
                    _spriteBatch.Draw(shieldTexture, new Vector2((0 + posModifier), _graphics.PreferredBackBufferHeight - (int)(_graphics.PreferredBackBufferHeight * 0.2f)), Color.White);

                    posModifier += _graphics.PreferredBackBufferWidth / 3 - 150;
                } */

            }

            if (gameOver)
            {
                player.lives = 0;
                _spriteBatch.DrawString(textFont, "Game is over, the Aliens have defeated you! Press Escape to exit.", new Vector2(_graphics.PreferredBackBufferWidth *0.25f, _graphics.PreferredBackBufferHeight /2), Color.Black);
            }
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
