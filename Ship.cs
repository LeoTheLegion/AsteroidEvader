﻿using LeoTheLegion.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship
{
    public class Ship : WorldSpaceEntity
    {
        private static Vector2 _defaultPosition = new Vector2(640, 360);
        private int _speed;
        private int _radius= 30;
        private bool _hasControls;

        private Sprite _sprite;

        public Ship(int speed) : base()
        {
            this._position = _defaultPosition;
            this._speed = speed;
            this._sprite = (Sprite)Resources.Load("ship");
            this._hasControls = false;
        }

        public override void Update(ref GameTime gameTime)
        {
            if (!this._hasControls) return;

            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kState.IsKeyDown(Keys.Right) && this._position.X < 1280)
            {
                this._position.X += _speed * dt;
            }

            if (kState.IsKeyDown(Keys.Left) && this._position.X > 0)
            {
                this._position.X -= _speed * dt;
            }

            if (kState.IsKeyDown(Keys.Down) && this._position.Y < 720)
            {
                this._position.Y += _speed * dt;
            }

            if (kState.IsKeyDown(Keys.Up) && this._position.Y > 0)
            {
                this._position.Y -= _speed * dt;
            }
        }
        public override void Render(ref SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_sprite.GetTexture2D(), this._position - new Vector2(34, 50), Color.White);
        }

        public void EnableControls()
        {
            this._hasControls = true;
        }

        public void DisableControls()
        {
            this._hasControls = false;
        }

        public void ResetPosition()
        {
            this._position = _defaultPosition;
        }
    }
}
