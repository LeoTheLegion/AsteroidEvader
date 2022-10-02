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

        public Asteroid(int speed)
        {
            this._speed = speed;
            this._sprite = (Sprite)Resources.Load("asteroid");
            Random random = new Random();
            this._position = new Vector2(1380, random.Next(0,721));
            this._circleCollider = new CircleCollider();
        }

        public override void Update(ref GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            this._position.X -= _speed * dt;
        }

        public override void Render(ref SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_sprite.GetTexture2D(),
                    this._position - new Vector2(59, 59),
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
