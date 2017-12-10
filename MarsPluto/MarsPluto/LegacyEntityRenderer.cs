using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace MarsPluto
{
    class LegacyEntityRenderer : EntityRenderer
    {
        public LegacyEntityRenderer(GameWindow window, Camera camera, Shader shader) : base(window, camera, shader)
            {

        }

        public override void begin()
        {
            
            GL.Enable(EnableCap.Texture2D);
        }
        public override void end()
        {
            
        }

        public void prepare(Entity entity)
        {



        }

        public override void render(Entity entity)
        {
            if (entity.x + entity.width > Camera.Position.X &&
                entity.x < Camera.Position.X + Window.Width &&
                entity.y + entity.height > Camera.Position.Y &&
                entity.y < Camera.Position.Y + Window.Height)
            {
                //TODO: FIX THIS 
                GL.BindTexture(TextureTarget.Texture2D, entity.texture);
                
                GL.Begin(PrimitiveType.Quads);


                GL.Vertex3(entity.x - Camera.Position.X, entity.y + entity.height - Camera.Position.Y, 0);
                GL.TexCoord2(0, 0);

                GL.Vertex3(entity.x - Camera.Position.X, entity.y - Camera.Position.Y, 0);
                GL.TexCoord2(0, 1);


                GL.Vertex3(entity.x + entity.width - Camera.Position.X, entity.y - Camera.Position.Y, 0);
                GL.TexCoord2(1, 1);

                GL.Vertex3(entity.x + entity.width - Camera.Position.X, entity.y + entity.height - Camera.Position.Y, 0);
                GL.TexCoord2(1, 0);

                GL.End();
            }
        }

    }

}
