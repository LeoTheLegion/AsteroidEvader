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
        private float _rotation;
        private float _rotationRate;
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

            this._rotation = RANDOM.Next(0, 360);
            int rate = 120;
            this._rotationRate = RANDOM.Next(-rate, rate);

            this._circleCollider = new CircleCollider();
        }

        public override void Update(ref GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            this._position.X -= _speed * dt;
            this._rotation += this._rotationRate * dt;
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
                MathHelper.ToRadians(this._rotation),
                origin,
                1,
                SpriteEffects.None,
                0);
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
