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
    public class Bullet : WorldSpaceEntity , ICollide
    {
        private const int _SPEED = 180;
        private const float _LIFESPAN = 10f;
        private float _life;
        private CircleCollider _circleCollider;

        public Bullet() : base()
        {
            this._sprite = (Sprite)Resources.Load("laser");
            this._life = _LIFESPAN;
            this._circleCollider = new CircleCollider();
        }

        public override void Update(ref GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            this._position.X += _SPEED * dt;

            this._life -= dt;

            if (this._life < 0) this.Destroy();
        }
        public override void Render(ref SpriteBatch _spriteBatch)
        {
            Texture2D tex = _sprite.GetTexture2D();
            Vector2 origin = new Vector2(tex.Width / 2, tex.Height / 2);

            _spriteBatch.Draw(
                tex,
                this._position,
                null,
                Color.White,
                MathHelper.ToRadians(90),
                origin,
                1,
                SpriteEffects.None,
                0);
        }

        public bool IsColliderActive()
        {
            return this._active;
        }

        public Collider GetCollider()
        {
            this._circleCollider.radius = 5;
            this._circleCollider.position = this._position;
            return this._circleCollider;
        }

        public void hit(ICollide collide)
        {
            if(collide is Asteroid)
            {
                Asteroid asteroid = (Asteroid)collide;
                asteroid.Destroy();
            }
        }

        public Entity GetEntity()
        {
            return this;
        }
    }
}
