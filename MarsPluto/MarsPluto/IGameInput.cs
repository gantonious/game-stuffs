using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsPluto
{
    public interface IGameInput: IDisposable
    {
        float getHorizontal();
        float getVertical();
        void poll();
    }
}
