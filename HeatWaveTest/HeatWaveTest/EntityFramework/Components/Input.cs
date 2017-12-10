using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatWaveTest.EntityFramework.Components
{
    class Input : Component
    {
        public IGameInput GameInput { get; set; }

        public Input(IGameInput gameInput)         
        {
            GameInput = gameInput;
        }
    }
}
