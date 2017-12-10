using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
namespace MarsPluto
{
    class TexturedRegion
    {
        private float width;
        private float height;
        private float tileWidth;
        private float tileHeight;

        public TexturedRegion(float width, float height, float tileWidth, float tileHeight)
        {
            this.width = width;
            this.height = height;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
        }

        public Vector4 getTexCoords(int tileColumn, int tileRow)
        {
            return new Vector4((tileColumn * tileWidth) / width,
                                (tileRow * tileHeight) / height,
                                (tileColumn * tileWidth + tileWidth) / width,
                                (tileRow * tileHeight + tileHeight) / height);
        }
    }
}
