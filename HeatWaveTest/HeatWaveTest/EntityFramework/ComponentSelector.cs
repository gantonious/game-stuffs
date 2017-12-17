using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatWaveTest.EntityFramework
{
    public class ComponentSelector
    {
        public long Mask;

        public ComponentSelector(params Type[] typesToSelect)
        {
            Mask = typesToSelect.Aggregate(0, (m, t) => m | 1 << Entity.GetComponentIndex(t));
        }
    }
}
