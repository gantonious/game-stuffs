using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace HeatWaveTest
{
    class Controller : IGameInput
    {
        private int joystickIndex;
        private JoystickState state;

        public Controller()
        {
            this.initalize();
        }

        private void initalize()
        {
            this.joystickIndex = 0;
            this.poll();
        }

        public void Dispose()
        {
            return;
        }

        public float GetHorizontal()
        {
            poll();
            float hor = this.state.GetAxis(JoystickAxis.Axis0);
            return (Math.Abs(hor) > 0.2) ? hor : 0;
        }

        public float GetVertical()
        {
            poll();
            float ver = this.state.GetAxis(JoystickAxis.Axis1);
            return (Math.Abs(ver) > 0.2) ? ver : 0;
        }

        public void poll()
        {
            this.state = Joystick.GetState(this.joystickIndex);
        }
    }
}
