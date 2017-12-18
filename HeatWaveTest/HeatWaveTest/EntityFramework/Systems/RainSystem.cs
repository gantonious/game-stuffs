using HeatWaveTest.EntityFramework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatWaveTest.EntityFramework.Systems
{
    public class RainSystem : System
    {
        public override ComponentSelector ComponentSelector { get; } = 
            new ComponentSelector(typeof(Rain), typeof(Velocity));

        public override void Process(Entity entity)
        {
            var velocity = entity.GetComponent<Velocity>();
            velocity.YVelocity += entity.EntityID * 0.0001f;
        }
    }
}
