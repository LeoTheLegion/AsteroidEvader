using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship
{
    public class Ship
    {
        public static Vector2 defaultPosition = new Vector2(640, 360);
        public Vector2 position = defaultPosition;
        public int _speed = 180;
        public int radius= 30;

        public void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kState.IsKeyDown(Keys.Right) && position.X < 1280)
            {
                position.X += _speed * dt;
            }

            if (kState.IsKeyDown(Keys.Left) && position.X > 0)
            {
                position.X -= _speed * dt;
            }

            if (kState.IsKeyDown(Keys.Down) && position.Y < 720)
            {
                position.Y += _speed * dt;
            }

            if (kState.IsKeyDown(Keys.Up) && position.Y > 0)
            {
                position.Y -= _speed * dt;
            }
        }
    }
}
