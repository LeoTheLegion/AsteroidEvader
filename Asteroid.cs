using LeoTheLegion.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship
{
    public class Asteroid : WorldSpaceEntity
    {
        private int _speed;
        private int _radius = 59;
        private Sprite _sprite;

        public Asteroid(int speed)
        {
            this._speed = speed;
            this._sprite = (Sprite)Resources.Load("asteroid");
            Random random = new Random();
            this._position = new Vector2(1380, random.Next(0,721));
        }

        public override void Update(ref GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            this._position.X -= _speed * dt;
        }

        public override void Render(ref SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_sprite.GetTexture2D(),
                    this._position - new Vector2(this._radius, this._radius),
                    Color.White);
        }
    }
}
