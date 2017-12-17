using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using HeatWaveTest.EntityFramework.Components;

namespace HeatWaveTest.EntityFramework.Systems
{
    public class MovementSystem : System
    {
        public ComponentSelector ComponentSelector = new ComponentSelector(typeof(Velocity), typeof(Position));
        public MovementSystem() { }

        public override void Process(Entity entity)
        {
            if (entity.HasComponentsFor(ComponentSelector))
            {
                Velocity velocity = entity.GetComponent<Velocity>();
                Position position = entity.GetComponent<Position>();

                position.X += velocity.XVelocity;
                position.Y += velocity.YVelocity;
            }
        }
    }
}
