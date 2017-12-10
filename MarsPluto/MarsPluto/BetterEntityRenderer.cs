using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace MarsPluto
{

    class BetterEntityRenderer : EntityRenderer
    {

        IVertexData vao;
        Vector3[] vertBuffer;
        Vector2[] texBuffer;
        int[] ibo;

        Dictionary<Tuple<Vector3, Vector2>, int> vertexCache;

        int maxSize = 100000;
        int idx = 0;
        int flushNum = 0;
        int tex = 1;
            

        public BetterEntityRenderer(GameWindow window, Camera camera, Shader shader) : base(window, camera, shader)
        {
            vao = new VAO();
            vertBuffer = new Vector3[maxSize];
            texBuffer = new Vector2[maxSize];

            //ibo = new int[maxSize];
            //vertexCache = new Dictionary<Tuple<Vector3, Vector2>, int>();


        }

        public override void begin()
        {

        }

        public override void end()
        {
            flush();
            flushNum = 0;  
        }

        public void flush()
        {
            flushNum += 1;

            vao.registerAttribute(0, vertBuffer, Vector3.SizeInBytes, 3);
            vao.registerAttribute(1, texBuffer, Vector2.SizeInBytes, 2);

            vao.bind();

            Shader.start();
            
            Matrix4 translationToOrigin = Matrix4.CreateTranslation(-1*vertBuffer[0].X,
                                                                    -1*vertBuffer[0].Y, 0);
            Matrix4 scale = Matrix4.CreateScale(2*Camera.Zoom / (float)Window.Width,
                                                2*Camera.Zoom / (float)Window.Height, 1);
            Matrix4 translationToFinalSpot = Matrix4.CreateTranslation(2 * Camera.Zoom * (vertBuffer[0].X - Camera.Position.X - Window.Width / 2) / Window.Width, 
                                                                        2 * Camera.Zoom * (vertBuffer[0].Y - Camera.Position.Y - Window.Height / 2) / Window.Height, 0);

            Matrix4 world = Matrix4.Mult(translationToOrigin, scale);
            world = Matrix4.Mult(world, translationToFinalSpot);

            Shader.loadWorldRef(world);
            
            GL.BindTexture(TextureTarget.Texture2D, tex);
            GL.DrawArrays(PrimitiveType.Quads, 0, idx);


            Shader.stop();
            vao.unbind();

            idx = 0;
        }

        public override void render(Entity entity)
        {
            if (entity.x + entity.width > Camera.Position.X &&
                    entity.x < Camera.Position.X + Window.Width &&
                    entity.y + entity.height > Camera.Position.Y &&
                    entity.y < Camera.Position.Y + Window.Height)
            {
                if (entity.texture == tex && idx + 4 <= maxSize)
                {

                   
                    vertBuffer[idx] = new Vector3(entity.x, entity.y, 0);
                    vertBuffer[idx + 1] = new Vector3(entity.x + entity.width, entity.y, 0);
                    vertBuffer[idx + 2] = new Vector3(entity.x + entity.width, entity.y + entity.height, 0);
                    vertBuffer[idx + 3] = new Vector3(entity.x, entity.y + entity.height, 0);

                    /*
                    float transformedX = 2 * (entity.x - Camera.Position.X - window.Width / 2) / window.Width;
                    float transformedY = 2 * (entity.y - Camera.Position.Y - window.Height / 2) / window.Height;
                    float transformedWidth = 2 * entity.width / window.Width;
                    float transformedHeight = 2 * entity.height / window.Height;

                    vertBuffer[idx] = new Vector3(transformedX, transformedY, 0);
                    vertBuffer[idx + 1] = new Vector3(transformedX + transformedWidth, transformedY, 0);
                    vertBuffer[idx + 2] = new Vector3(transformedX + transformedWidth, transformedY + transformedHeight, 0);
                    vertBuffer[idx + 3] = new Vector3(transformedX, transformedY + transformedHeight, 0);
                    */

                    texBuffer[idx] = new Vector2(entity.U1, entity.V2);
                    texBuffer[idx + 1] = new Vector2(entity.U2, entity.V2);
                    texBuffer[idx + 2] = new Vector2(entity.U2, entity.V1);
                    texBuffer[idx + 3] = new Vector2(entity.U1, entity.V1);
                    
                    /*
                    Tuple<Vector3, Vector2>[] tupleKeys = new Tuple<Vector3, Vector2>[4];
                    for (int i = 0; i < 4; i++) tupleKeys[i] = Tuple.Create(vertBuffer[idx + i], texBuffer[idx + i]);

                    if (!vertexCache.ContainsKey(tupleKeys[0])) vertexCache[tupleKeys[0]] = idx;
                    if (!vertexCache.ContainsKey(tupleKeys[1])) vertexCache[tupleKeys[1]] = idx + 1;
                    if (!vertexCache.ContainsKey(tupleKeys[2])) vertexCache[tupleKeys[2]] = idx + 2;
                    if (!vertexCache.ContainsKey(tupleKeys[3])) vertexCache[tupleKeys[3]] = idx + 3;

                    ibo[idx] = vertexCache[tupleKeys[0]];
                    ibo[idx + 1] = vertexCache[tupleKeys[1]];
                    ibo[idx + 2] = vertexCache[tupleKeys[2]];
                    ibo[idx + 3] = vertexCache[tupleKeys[3]];
                    */

                    idx += 4;
                }
                else
                {
                    flush();
                    tex = entity.texture;
                    render(entity);
                }
            }
        }
    }
}


