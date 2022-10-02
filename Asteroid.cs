using LeoTheLegion.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spaceship.Core.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship
{
    public class Asteroid : WorldSpaceEntity , ICollide
    {
        private int _speed;
        private CircleCollider _circleCollider;
        private static Random RANDOM = new Random();
        public Asteroid(int speed)
        {
            this._speed = speed;
            this._sprite = (Sprite)Resources.Load("asteroid");
            this._position = new Vector2(
                Game1.WIDTH + 100,
                RANDOM.Next(0,Game1.HEIGHT + 1)
                );
            this._circleCollider = new CircleCollider();
        }

        public override void Update(ref GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            this._position.X -= _speed * dt;
        }

        public override void Render(ref SpriteBatch _spriteBatch)
        {
            Texture2D tex = _sprite.GetTexture2D();
            Vector2 offset = new Vector2(tex.Width / 2, tex.Height / 2);
            _spriteBatch.Draw(tex,
                    this._position - offset,
                    Color.White);
        }

        public bool IsColliderActive()
        {
            return this.GetActive();
        }

        public Collider GetCollider()
        {
            this._circleCollider.radius = 59;
            this._circleCollider.position = this._position;
            return this._circleCollider;
        }

        public void hit(ICollide collide)
        {

        }
    }
}
