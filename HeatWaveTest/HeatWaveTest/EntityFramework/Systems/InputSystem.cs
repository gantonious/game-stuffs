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
        public ComponentSelector ComponentSelector = new ComponentSelector(typeof(Velocity), typeof(Input));

        public override void Process(Entity entity)
        {
            if (entity.HasComponentsFor(ComponentSelector)) {
                Input input = entity.GetComponent<Input>();
                Velocity velocity = entity.GetComponent<Velocity>();

                velocity.XVelocity = 10 * input.GameInput.GetHorizontal();
                velocity.YVelocity = -10 * input.GameInput.GetVertical();
            }
        }
    }
}
