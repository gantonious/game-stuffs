using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatWaveTest.EntityFramework
{
    public class Engine
    {
        private List<Entity> _entities;
        private List<System> _logicalSystems;
        private List<System> _renderingSystems;

        private Dictionary<string, int> _taggedEntities;

        public Engine()
        {
            _entities = new List<Entity>();
            _logicalSystems = new List<System>();
            _renderingSystems = new List<System>();
            _taggedEntities = new Dictionary<string, int>();
        }

        public Entity CreateEntity()
        {
            var newEntity = new Entity(_entities.Count);
            _entities.Add(newEntity);
            return newEntity;
        }

        public void TagEntity(Entity entity, string name)
        {
            _taggedEntities[name] = entity.EntityID;
        }

        public Entity GetTaggedEntity(string name)
        {
            return _entities[_taggedEntities[name]];
        }

        public void RegisterLogicalSystem(System system)
        {
            system.Engine = this;
            _logicalSystems.Add(system);
        }

        public void RegisterRenderingSystem(System system)
        {
            system.Engine = this;
            _renderingSystems.Add(system);
        }

        public void Update(double delta)
        {
            Parallel.ForEach(_logicalSystems, l => l.Begin());

            Parallel.ForEach(_entities, e =>
            {
                foreach (var logicalSystem in _logicalSystems)
                if (e.HasComponentsFor(logicalSystem.ComponentSelector))
                {
                    logicalSystem.Process(e);
                }
            });

            Parallel.ForEach(_logicalSystems, l => l.End());
        }

        public void Render()
        {
            foreach (var renderingSystem in _renderingSystems)
            {
                renderingSystem.Begin();
            }

            foreach (var entity in _entities)
            foreach (var renderingSystem in _renderingSystems)
            if (entity.HasComponentsFor(renderingSystem.ComponentSelector))
            {
                renderingSystem.Process(entity);
            }

            foreach (var renderingSystem in _renderingSystems)
            {
                renderingSystem.End();
            }
        }
    }
}
