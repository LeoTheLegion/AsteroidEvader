using LeoTheLegion.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spaceship.Core.Collision;
using System;
using System.Collections.Generic;

namespace Spaceship
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Controller _gameController;

        private static Game1 _INSTANCE;
        private EntityManagementSystem _entityManagementSystem;
        private CollisionManagementSystem _collisionManagementSystem;

        public static int WIDTH
        {
            get { return _INSTANCE._graphics.PreferredBackBufferWidth; }
        }

        public static int HEIGHT
        {
            get { return _INSTANCE._graphics.PreferredBackBufferHeight; }
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _INSTANCE = this;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            _entityManagementSystem = new EntityManagementSystem();
            _collisionManagementSystem = new CollisionManagementSystem();

            _entityManagementSystem._OnRegister += _collisionManagementSystem.Register;
            _entityManagementSystem._OnUnregister += _collisionManagementSystem.Unregister;

            Resources.Init(new Dictionary<string, Asset>()
            {
                {"space" , new Sprite("space") },
                {"ship" , new Sprite("ship") },
                {"asteroid" , new Sprite("asteroid") },
                {"spaceFont" , new Font("spaceFont") },
                {"timerFont" , new Font("timerFont") },
            });

            new Decorative("space", new Vector2(0, 0)).SetSort(-1);
            ;

            _gameController = new Controller(
                (Ship)new Ship(180).SetSort(0),
                (Text)new Text("timerFont","",new Vector2(3,3)).SetSort(1),
                (Text)new Text("spaceFont", "Press Enter to Begin", new Vector2(0, 0)).SetSort(1)
                );


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            Resources.LoadContent(Content);

            _gameController.OnGameover();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            _entityManagementSystem.Update(ref gameTime);
            _collisionManagementSystem.CheckForCollisions();

            _gameController.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _entityManagementSystem.Render(ref _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}