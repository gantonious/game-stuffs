using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeatWave.Graphics;

namespace MarsPluto
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using OpenTK;
    using OpenTK.Graphics.OpenGL;

    namespace MarsPluto
    {
        struct VAO
        {

        }
        class FasterEntityRenderer : EntityRenderer
        {


            public FasterEntityRenderer(GameWindow window, Camera camera, Shader shader) : base(window, camera, shader)
            {

            }

            public override void begin()
            {
                //Drawing.initDrawing();
          
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
                    GL.Color3(Color.Black);
                    //Drawing.drawRect(entity.x - Camera.Position.X, entity.y - Camera.Position.Y, entity.width, entity.height, entity.texture);
                }
            }

        }

    }

}
