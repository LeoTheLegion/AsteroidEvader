using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship.Core.Collision
{
    public class CircleCollider : Collider
    {
        public Vector2 position { set; get; }
        public float radius { set; get; }
    }
}
