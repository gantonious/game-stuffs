using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace HeatWaveTest
{
    class KeyboardAndMouse : IGameInput
    {
        KeyboardState state;

        public void Dispose()
        {
            return;
        }

        public float GetHorizontal()
        {
            poll();
            if (state.IsKeyDown(Key.D))
            {
                return 1;
            }
            else if (state.IsKeyDown(Key.A))
            {
                return -1;
            }
            return 0;
        }

        public float GetVertical()
        {
            poll();
            if (state.IsKeyDown(Key.W))
            {
                return 1;
            }
            else if (state.IsKeyDown(Key.S))
            {
                return -1;
            }
            return 0;
        }

        public void poll()
        {
            this.state = Keyboard.GetState();
        }
    }
}
