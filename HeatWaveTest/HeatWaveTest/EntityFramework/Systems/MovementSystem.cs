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
        public MovementSystem() { }

        public override void Process(Entity entity)
        {
            Velocity velocity = entity.GetComponent<Velocity>();
            Position position = entity.GetComponent<Position>();

            if (velocity == null || position == null) return;

            position.X += velocity.XVelocity;
            position.Y += velocity.YVelocity;
        }
    }
}
