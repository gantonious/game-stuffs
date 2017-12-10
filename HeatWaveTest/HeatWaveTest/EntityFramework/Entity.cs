using System;
using System.Collections.Generic;

namespace HeatWaveTest.EntityFramework
{
    public class Entity
    {
        public int EntityID { get; private set; }
        public Dictionary<Type, Component> ComponentMap { get; private set; }

        public Entity(int entityID)
        {
            EntityID = entityID;
            ComponentMap = new Dictionary<Type, Component>();
        }

        public void AddComponent(Component component)
        {
            ComponentMap[component.GetType()] = component;
        }

        public void RemoveComponent<T>()
        {
            if (!ComponentMap.ContainsKey(typeof(T))) return;
            ComponentMap.Remove(typeof(T));
        }

        public T GetComponent<T>() where T : Component
        {
            Component output;
            ComponentMap.TryGetValue(typeof(T), out output);
            return (T)output;
        }
    }
}
