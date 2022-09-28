using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship
{
    public class Asteroid
    {
        public Vector2 position = new Vector2(600, 300);
        public int speed;
        public int radius = 59;

        public Asteroid(int speed)
        {
            this.speed = speed;
            Random random = new Random();
            this.position = new Vector2(1380, random.Next(0,721));
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            position.X -= speed * dt;
        }
    }
}
