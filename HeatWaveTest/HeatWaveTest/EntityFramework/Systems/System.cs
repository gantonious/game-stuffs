using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatWaveTest.EntityFramework
{
    public abstract class System
    {
        public abstract ComponentSelector ComponentSelector { get; }

        public virtual void Begin() { }
        public virtual void End() { }
        public abstract void Process(Entity entity);
    }
}
