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
        public override void Process(Entity entity)
        {
            Input input = entity.GetComponent<Input>();
            Velocity velocity = entity.GetComponent<Velocity>();

            if (input == null || velocity == null) return;

            velocity.XVelocity = 10 * input.GameInput.GetHorizontal();
            velocity.YVelocity = -10 * input.GameInput.GetVertical();
        }
    }
}
