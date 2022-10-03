using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeoTheLegion.Core
{
    public class EntityGroup
    {
        private List<Entity> _entities;

        public EntityGroup()
        {
            _entities = new List<Entity>();
        }

        public EntityGroup(List<Entity> entities)
        {
            _entities = entities;
        }

        public EntityGroup(Entity[] entities)
        {
            this._entities = new List<Entity>(entities);
        }

        public void Add(Entity e)
        {
            e._OnDestroy += this.Remove;
            this._entities.Add(e);
        }

        public void Remove(Entity e)
        {
            this._entities.Remove(e);
        }

        public List<Entity> GetList() { return _entities; }

        public void SetActive(bool active)
        {
            foreach (var e in this._entities)
            {
                e.SetActive(active);
            }
        }

        public void Destroy()
        {
            foreach (var e in this._entities)
            {
                e._OnDestroy -= this.Remove;
                e.Destroy();
            }
            this._entities.Clear();
        }
    }
}
