﻿using LeoTheLegion.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship.Core.Collision
{
    public class CollisionManagementSystem
    {
        private  List<ICollide> _collides = new List<ICollide>();

        public CollisionManagementSystem()
        {
        }

        public void Register(Entity e)
        {
            if(e is ICollide)
                _collides.Add((ICollide)e);
        }
        public void Unregister(Entity e)
        {
            if (e is ICollide)
                _collides.Remove((ICollide)e);
        }
        
        public void CheckForCollisions()
        {
            for (int i = 0; i < _collides.Count; i++)
            {
                for (int j = 0; j < _collides.Count; j++)
                {
                    if (i == j) continue;
                    if (_collides[i].IsColliderActive() == false) continue;

                    if(ResolveCollision(_collides[i], _collides[j]))
                        _collides[i].hit(_collides[j]);
                }
            }
        }

        private bool ResolveCollision(ICollide collide1, ICollide collide2)
        {
            Collider col1 = collide1.GetCollider();
            Collider col2 = collide2.GetCollider();

            if(col1 is CircleCollider && col2 is CircleCollider)
            {
                return CircleCircleCollision((CircleCollider)col1, (CircleCollider)col2);
            }

            return false;
        }

        private bool CircleCircleCollision(CircleCollider col1, CircleCollider col2)
        {
            float sum = col1.radius + col2.radius;

            return (Vector2.Distance(col1.position, col2.position) < sum);
        }
    }
}
