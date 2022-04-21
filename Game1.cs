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
            
           

            for (int i = 0; i < 6; i++)
            {
                Alien newAlien = new Alien();

                newAlien.position.X = startingPosition;
                newAlien.position.Y = 50;

                aliens.Add(newAlien);

                startingPosition += 50;
            }
                

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            alienTexture = Content.Load<Texture2D>("Alien");

            /* bulletTexture = Content.Load<Texture2D>("bullet");
            playerTexture = Content.Load<Texture2D>("player");
            shieldTexture = Content.Load<Texture2D>("shield");
            gameFont = Content.Load<SpriteFont>("gameFont"); */

           
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            for (int i = 0; i < aliens.Count; i++)
            {
                aliens[i].Update(gameTime);
            }

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
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
