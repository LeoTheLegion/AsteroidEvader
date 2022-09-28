using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Spaceship
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _shipSprite,_asteroidSprite,_spaceSprite;
        private SpriteFont _gameFont,_timerFont;

        private Ship player = new Ship();
        private Controller _gameController = new Controller();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _shipSprite = Content.Load<Texture2D>("ship");
            _asteroidSprite = Content.Load<Texture2D>("asteroid");
            _spaceSprite = Content.Load<Texture2D>("space");

            _gameFont = Content.Load<SpriteFont>("spaceFont");
            _timerFont = Content.Load<SpriteFont>("timerFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if(_gameController.inGame)
                player.Update(gameTime);

            _gameController.Update(gameTime);

            for (int i = 0; i < _gameController.asteroids.Count; i++)
            {
                _gameController.asteroids[i].Update(gameTime);

                int sum = _gameController.asteroids[i].radius + player.radius;

                if(Vector2.Distance(_gameController.asteroids[i].position,player.position) < sum)
                {
                    _gameController.inGame = false;
                    player.position = Ship.defaultPosition;
                    _gameController.asteroids.Clear();
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(_spaceSprite, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(_shipSprite, player.position - new Vector2(34,50), Color.White);

            for (int i = 0; i < _gameController.asteroids.Count; i++)
            {
                _spriteBatch.Draw(_asteroidSprite,
                    _gameController.asteroids[i].position - new Vector2(_gameController.asteroids[i].radius,
                    _gameController.asteroids[i].radius),
                    Color.White);
            }

            if(_gameController.inGame == false)
            {
                string menuMessage = "Press Enter to Begin";

                Vector2 sizeOfText = _gameFont.MeasureString(menuMessage);
                int halfWidth = _graphics.PreferredBackBufferWidth / 2;

                _spriteBatch.DrawString(_gameFont, menuMessage, new Vector2(halfWidth, 200) - sizeOfText/2, Color.White);
            }

            _spriteBatch.DrawString(
                _timerFont,
                "Time: " + Math.Floor(_gameController.totalTime).ToString(),
                new Vector2(3,3),
                Color.White
                );

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}