using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.ES20;
using OpenTK;

namespace MarsPluto
{
    class VBO : IVertexData, IDisposable
    {
        private int vboID;
        private List<int> attributeIDs = new List<int>();

        public VBO()
        {
            this.vboID = GL.GenBuffer();
        }


        public void registerAttribute<T>(int attributeID, T[] data, int elementSizeinBytes, int elementSize) where T : struct
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(data.Length * elementSizeinBytes), data, BufferUsageHint.StaticDraw);
            if (attributeID == 0)
            {
                GL.VertexAttribPointer(attributeID, elementSize, VertexAttribPointerType.Float, false, 0, 0);
            }
            else
            {
                GL.VertexAttribPointer(attributeID, elementSize, VertexAttribPointerType.Float, false, 0, data.Length * Vector2.SizeInBytes);
            }
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void Dispose()
        {
            GL.DeleteBuffer(vboID);

       }

        public void bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            foreach (int attributeID in attributeIDs) GL.EnableVertexAttribArray(attributeID);
        }

        public void unbind()
        {
            foreach (int attributeID in attributeIDs) GL.DisableVertexAttribArray(attributeID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}
