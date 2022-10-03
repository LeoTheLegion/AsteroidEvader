using LeoTheLegion.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spaceship.Core.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship
{
    public class Ship : WorldSpaceEntity, ICollide
    {
        private static Vector2 _defaultPosition {
            get { return new Vector2(Game1.WIDTH/2,Game1.HEIGHT/2); }
        }
        private int _speed;
        private bool _hasControls,_canShoot;
        private CircleCollider _circleCollider;

        public Ship(int speed) : base()
        {
            this._position = _defaultPosition;
            this._speed = speed;
            this._sprite = (Sprite)Resources.Load("ship");
            this._hasControls = false;
            this._canShoot = false;
            this._circleCollider = new CircleCollider();
        }

        public override void Update(ref GameTime gameTime)
        {
            if (!this._hasControls) return;

            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kState.IsKeyDown(Keys.Right) && this._position.X < Game1.WIDTH)
            {
                this._position.X += _speed * dt;
            }

            if (kState.IsKeyDown(Keys.Left) && this._position.X > 0)
            {
                this._position.X -= _speed * dt;
            }

            if (kState.IsKeyDown(Keys.Down) && this._position.Y < Game1.HEIGHT)
            {
                this._position.Y += _speed * dt;
            }

            if (kState.IsKeyDown(Keys.Up) && this._position.Y > 0)
            {
                this._position.Y -= _speed * dt;
            }

            if (kState.IsKeyDown(Keys.Space) && _canShoot)
            {
                CreateBullet(this._position);
                this._canShoot = false;
            }
        }
        public override void Render(ref SpriteBatch _spriteBatch)
        {
            Texture2D tex = _sprite.GetTexture2D();
            Vector2 offset = new Vector2(tex.Width/2, tex.Height/2);
            _spriteBatch.Draw(tex, this._position - offset, Color.White);
        }

        public void CreateBullet(Vector2 position)
        {
            Bullet bullet = new Bullet();

            bullet.SetPosition(position);
        }

        public void EnableControls()
        {
            this._hasControls = true;
        }

        public void DisableControls()
        {
            this._hasControls = false;
        }

        public void Reset()
        {
            this._position = _defaultPosition;
            this._canShoot = false;
        }

        public bool IsColliderActive()
        {
            return this.GetActive();
        }

        public Collider GetCollider()
        {
            this._circleCollider.position = this._position;
            this._circleCollider.radius = 30;
            return this._circleCollider;
        }

        public void hit(ICollide collide)
        {
            if (collide is Battery)
            {
                this._canShoot = true;
                collide.GetEntity().Destroy();
            }
            else if(collide is Asteroid) 
            { 
                Controller.GameOver(); 
            }
                
        }

        public Entity GetEntity()
        {
            return this;
        }
    }
}
