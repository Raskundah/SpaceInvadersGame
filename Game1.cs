using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;


namespace SpaceInvadersGame
{
    public class Game1 : Game
    {


        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D alienTexture;
        private Texture2D bulletTexture;
        private Texture2D playerTexture;
        private Texture2D shieldTexture;
        private SpriteFont gameFont;
        private int score;
        public bool gameOver = false;

        private int[] highScore = new int[5] { 0, 0, 0, 0, 0 };


        
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
           
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.ApplyChanges();
            player = new Player(_graphics);
            gameController = new Controller();

            

            int startingPosition = 50;
            int yPos = 50;
            
           

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
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            alienTexture = Content.Load<Texture2D>("Alien");
            bulletTexture = Content.Load<Texture2D>("Bullet");
            playerTexture = Content.Load<Texture2D>("Ship");
            shieldTexture = Content.Load<Texture2D>("shield");
            // gameFont = Content.Load<SpriteFont>("gameFont"); 

           
        }

        protected override void Update(GameTime gameTime)
        {


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            bool hasReached = false;
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

            player.PlayerUpdate(gameTime);
            
            base.Update(gameTime);

            foreach(Bullet bullets in Bullet.bullets)
            {
                bullets.BulletUpdate(gameTime);
            }


        }

        protected override void Draw(GameTime gameTime)

        {


            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            for (int i = 0; i < Alien.aliens.Count; i++)
            {
                _spriteBatch.Draw(alienTexture, Alien.aliens[i].position, Color.White);

            }

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
