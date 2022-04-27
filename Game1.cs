using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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
        private int[] highScore = new int[5] { 0, 0, 0, 0, 0 };
        List<Alien> aliens= new List<Alien>();


        Bullet bullet = new Bullet();
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

                
                    
                aliens.Add(newAlien);

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
            for (int i = 0; i < aliens.Count; i++)
            {
                if (aliens[i].Update(gameTime, alienTexture.Width) && !hasReached)
                {
                    hasReached = true;
                }
            }

            if (hasReached)
            {
                foreach (Alien alien in aliens)
                {
                    alien.position.Y += 50;
                    alien.speed *= -1;
                    alien.Update(gameTime, alienTexture.Width);


                }
            }

            player.PlayerUpdate(gameTime);
            bullet.BulletUpdate(gameTime); 
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)

        {


            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            for (int i = 0; i < aliens.Count; i++)
            {
                _spriteBatch.Draw(alienTexture, aliens[i].position, Color.White);

            }

            _spriteBatch.Draw(playerTexture, player.playerPosition, Color.White);

            if (bullet != null && bullet.isFired == true)
            {
                _spriteBatch.Draw(bulletTexture, new Vector2 (player.playerPosition.X + (playerTexture.Width/2) - 4, player.playerPosition.Y - 20), Color.White);

            }
            
            _spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
