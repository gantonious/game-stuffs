using System;
using System.Collections.Generic;
using System.Linq;

namespace HeatWaveTest.EntityFramework
{
    public class Entity
    {
        public static Dictionary<Type, int> ComponentIndicies { get; set; } = new Dictionary<Type, int>();

        public int EntityID { get; private set; }
        public Dictionary<Type, Component> ComponentMap { get; private set; }

        public long ComponentMask { get; set; }

        public Entity(int entityID)
        {
            EntityID = entityID;
            ComponentMap = new Dictionary<Type, Component>();
        }

        public static int GetComponentIndex(Type componentType)
        {
            if (!ComponentIndicies.ContainsKey(componentType))
            {
                ComponentIndicies[componentType] = ComponentIndicies.Count;
            }
            return ComponentIndicies[componentType];
        }

        public void AddComponent(Component component)
        {
            long componentMask = 1 << GetComponentIndex(component.GetType());
            ComponentMask |= componentMask;

            ComponentMap[component.GetType()] = component;
        }

        public void RemoveComponent<T>()
        {
            long componentMask = long.MaxValue & (0 << GetComponentIndex(typeof(T)));
            ComponentMask &= componentMask;

            if (!ComponentMap.ContainsKey(typeof(T))) return;
            ComponentMap.Remove(typeof(T));
        }

        public T GetComponent<T>() where T : Component
        {
            ComponentMap.TryGetValue(typeof(T), out Component output);
            return (T)output;
        }

        public bool HasComponentsFor(ComponentSelector componentSelector)
        {
            var selectedMask = componentSelector.Mask;
            return (selectedMask & ComponentMask) == selectedMask;
        }
    }
}
