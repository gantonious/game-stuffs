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

        public Engine()
        {
            _entities = new List<Entity>();
            _logicalSystems = new List<System>();
            _renderingSystems = new List<System>();
        }

        public Entity CreateEntity()
        {
            var newEntity = new Entity(_entities.Count);
            _entities.Add(newEntity);
            return newEntity;
        }

        public void RegisterLogicalSystem(System system)
        {
            _logicalSystems.Add(system);
        }

        public void RegisterRenderingSystem(System system)
        {
            _renderingSystems.Add(system);
        }

        public void Update(double delta)
        {
            foreach (var logicalSystem in _logicalSystems)
            {
                logicalSystem.Begin();
            }

            foreach (var entity in _entities)
            foreach (var logicalSystem in _logicalSystems)
            if (entity.HasComponentsFor(logicalSystem.ComponentSelector))
            {
                logicalSystem.Process(entity);
            }

            foreach (var logicalSystem in _logicalSystems)
            {
                logicalSystem.End();
            }
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
