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
    public class Battery : Asteroid 
    {
        public Battery(int speed) : base(speed)
        {
            this._sprite = (Sprite)Resources.Load("battery");
        }

        public override Collider GetCollider()
        {
            this._circleCollider.radius = 40;
            this._circleCollider.position = this._position;
            return this._circleCollider;
        }
    }
}
