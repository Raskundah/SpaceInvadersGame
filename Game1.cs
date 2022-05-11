using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;


namespace SpaceInvadersGame

    // coded by ian barrie
    // 11/05/2022
{
    public class Game1 : Game
    {
        // define initial variables

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D alienTexture;
        private Texture2D bulletTexture;
        private Texture2D playerTexture;
        private Texture2D shieldTexture;
        private SpriteFont textFont;
        private SpriteFont gameFont;
        private int score;
        public bool gameOver = false;

        // currently vestigial code for a high score system.

        private int[] highScore = new int[5] { 0, 0, 0, 0, 0 };


        //create instances of a class.
        Player player;
        Controller gameController;

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
            player = new Player(_graphics);
            gameController = new Controller();

            // define starting alien positions.

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
            }
             
            base.Initialize();
        }


        protected override void LoadContent()

            //load files from content folder into required addresses
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            alienTexture = Content.Load<Texture2D>("Alien");
            bulletTexture = Content.Load<Texture2D>("Bullet");
            playerTexture = Content.Load<Texture2D>("Ship");
            shieldTexture = Content.Load<Texture2D>("shield");
            textFont = Content.Load<SpriteFont>("timerFont");
            // gameFont = Content.Load<SpriteFont>("gameFont"); 
           
        }
        protected override void Update(GameTime gameTime)
        {


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

            if (hasReached)
            {
                foreach (Alien alien in Alien.aliens)
                {
                    alien.position.Y += 50;
                    alien.speed *= -1;
                    alien.Update(gameTime, alienTexture.Width);

                    if (alien.position.Y >= (int)_graphics.PreferredBackBufferHeight * 0.25)
                    {
                        gameOver = true;
                    }

                }
            }

            player.PlayerUpdate(gameTime); // calls frame updates for the player 
            
            base.Update(gameTime);

            foreach(Bullet bullets in Bullet.bullets)
            {
                bullets.BulletUpdate(gameTime); //uipdates each instance of bullet class.
            }

            foreach(Bullet bullets in Bullet.bullets) // collision code.
            {
                foreach (Alien aliens in Alien.aliens)
                {
                    int sum = bullets.radius + aliens.radius;
                    if (Vector2.Distance(bullets.bulletPosition, aliens.position) < sum)
                    { bullets.Collided = true;
                        aliens.Dead = true;
                        score += 100;
                    }
                
                }
            }

            Bullet.bullets.RemoveAll(p => p.Collided); // remove collided elements.
            Alien.aliens.RemoveAll(d => d.Dead);

        }
        protected override void Draw(GameTime gameTime)

        {


            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            for (int i = 0; i < Alien.aliens.Count; i++)
            {
                _spriteBatch.Draw(alienTexture, Alien.aliens[i].position, Color.White);

            }

            _spriteBatch.DrawString(textFont, "Score: " + score, new Vector2(10, 10), Color.White);

            _spriteBatch.Draw(playerTexture, player.playerPosition, Color.White);

            foreach (Bullet bullets in Bullet.bullets)
            {
                _spriteBatch.Draw(bulletTexture, new Vector2(bullets.bulletPosition.X + 27 , bullets.bulletPosition.Y - 20), Color.White);
            }

            int posModifier = _graphics.PreferredBackBufferWidth / 3 - 175;


            for (int i = 0; i < 3; ++i)
            {


                _spriteBatch.Draw(shieldTexture, new Vector2((0 + posModifier), _graphics.PreferredBackBufferHeight - (int)(_graphics.PreferredBackBufferHeight * 0.2f)), Color.White);

                posModifier += _graphics.PreferredBackBufferWidth / 3 - 150;
            }

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
