using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatWaveTest
{
    public interface IGameInput : IDisposable
    {
        float GetHorizontal();
        float GetVertical();
        void poll();
    }
}
