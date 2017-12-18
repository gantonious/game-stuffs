using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeatWaveTest.EntityFramework.Components;

namespace HeatWaveTest.EntityFramework.Systems
{
    class InputSystem : System
    {
        public override ComponentSelector ComponentSelector { get; } = 
            new ComponentSelector(typeof(Velocity), typeof(Input));

        public override void Process(Entity entity)
        {
            Input input = entity.GetComponent<Input>();
            Velocity velocity = entity.GetComponent<Velocity>();

            velocity.XVelocity = 10 * input.GameInput.GetHorizontal();
            velocity.YVelocity = -10 * input.GameInput.GetVertical();
        }
    }
}
