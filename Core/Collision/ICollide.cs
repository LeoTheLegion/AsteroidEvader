using LeoTheLegion.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship.Core.Collision
{
    public interface ICollide
    {
        bool IsColliderActive();
        Collider GetCollider();
        void hit(ICollide collide);
        Entity GetEntity();
    }
}
