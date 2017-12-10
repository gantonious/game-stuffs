using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsPluto
{
    interface IVertexData
    {
        void bind();
        void unbind();
        void registerAttribute<T>(int attributeID, T[] data, int elementSizeinBytes, int elementSize) where T : struct;
    }
}
