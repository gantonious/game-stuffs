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

    class DefaultEntityRenderer : EntityRenderer
    {
       
        IVertexData vao;
        Vector3[] vertBuffer;
        Vector2[] texBuffer;
        int maxSize = 4;

        public DefaultEntityRenderer(GameWindow window, Camera camera, Shader shader) : base(window, camera, shader)
        {

            vao = new VAO();
            vertBuffer = new Vector3[4]
            {
                new Vector3(0, 0, 0),
                new Vector3(1, 0, 0),
                new Vector3(1, 1, 0),
                new Vector3(0, 1, 0),

            };

            texBuffer = new Vector2[4]
            {
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(1, 0),
                new Vector2(0, 0),
            };

            Console.WriteLine((Vector2.SizeInBytes * 8 * 000)/ 1024);

            vao.registerAttribute(0, vertBuffer, Vector3.SizeInBytes, 3);
            vao.registerAttribute(1, texBuffer, Vector2.SizeInBytes, 2);

            /*
            VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vector2.SizeInBytes * vertBuffer.Length), vertBuffer, BufferUsageHint.StaticDraw);
            GL.VertexPointer(2, VertexPointerType.Float, Vector2.SizeInBytes * 2, 0);
            GL.TexCoordPointer(2, TexCoordPointerType.Float, Vector2.SizeInBytes * 2, Vector2.SizeInBytes);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);*/
        }

        public override void begin()
        {
            // enable needed client states
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);
            vao.bind();
            Shader.start();

        }
        public override void end()
        {
            vao.unbind();
            Shader.stop();
        }
        public void prepare()
        {

        }
        public override void render(Entity entity)
        {
            if (entity.x + entity.width > Camera.Position.X &&
                    entity.x < Camera.Position.X + Window.Width &&
                    entity.y + entity.height > Camera.Position.Y &&
                    entity.y < Camera.Position.Y + Window.Height)
            {

                Matrix4 translation = Matrix4.CreateTranslation(2 * (entity.x - Camera.Position.X - Window.Width / 2) / Window.Width, 2 * (entity.y - Camera.Position.Y - Window.Height / 2) / Window.Height, 0);
                Matrix4 scale = Matrix4.CreateScale(2 * entity.width / Window.Width, 2 * entity.height / Window.Height, 1);
                Matrix4 world = Matrix4.Mult(scale, translation);

                Shader.loadWorldRef(world);

                GL.BindTexture(TextureTarget.Texture2D, entity.texture);
                GL.DrawArrays(PrimitiveType.Quads, 0, 4);
            }



        }

    }

}
