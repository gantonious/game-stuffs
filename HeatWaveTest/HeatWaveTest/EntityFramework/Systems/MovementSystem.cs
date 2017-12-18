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
        public override ComponentSelector ComponentSelector { get; } = 
            new ComponentSelector(typeof(Velocity), typeof(Position));

        public override void Process(Entity entity)
        {
            Velocity velocity = entity.GetComponent<Velocity>();
            Position position = entity.GetComponent<Position>();

            position.X += velocity.XVelocity;
            position.Y += velocity.YVelocity;
        }
    }
}
