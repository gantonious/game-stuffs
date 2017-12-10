using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using MarsPluto.MarsPluto;
using OpenTK.Graphics;

namespace MarsPluto
{
    class test
    {
        private static int num = 2;

        public static void setNum(int num)
        {
            test.num = num;
        }

        public static int getNum()
        {
            return test.num;
        }
    }
    class MainGame : GameWindow
    {
        List<Entity> entities = new List<Entity>();
        Entity player;
        Goomba goomba;
        EntityRenderer entityRenderer;
        AssetLoader assetLoader = new AssetLoader();
        decimal lastFrame;
        Camera camera;
        Shader shader;
        int lastWidth;
        Entity text;


        public MainGame(int width, int height) : base(width, height) {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            
            lastWidth = width;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            lastFrame = DateTime.Now.Ticks / (decimal)TimeSpan.TicksPerMillisecond;
            // initialize entities

            shader = new Shader("Assets/Shaders/vertexShader.txt", "Assets/Shaders/fragmentShader.txt");
            

            //assetLoader.loadFont("Assets/Textures/ariali.ttf");

            
            //AudioPlayer.loadSound("Assets/Sound/Goldenrod City Acoustic.mp3");

            for (int i = 0; i<10000; i++)
            {
                this.entities.Add(new SolidBlock(200 + 25*i, 100, 25, 25, assetLoader.loadTexture("Assets/Textures/block.png")));
            }

            this.entities.Add(new SolidBlock(1000, 250, 25, 25, assetLoader.loadTexture("Assets/Textures/block.png")));
            this.entities.Add(new SolidBlock(200, 250, 25, 25, assetLoader.loadTexture("Assets/Textures/block.png")));
            text = new SolidBlock(300, 200, 1000, 40, assetLoader.loadFont("Assets/Textures/ariali.ttf", "text rendering test :D", 1000, 40, 20));
            this.player = new Player(50, 50, 64, 64, assetLoader.loadTexture("Assets/Textures/redspritesheet2.png"));
            this.entities.Add(new SolidBlock(75, 75, 25, 25, assetLoader.loadTexture("Assets/Textures/block.png")));
            this.camera = new Camera(this, player);

            this.camera.Zoom = 1f;
            this.entityRenderer = new BetterEntityRenderer(this, this.camera, this.shader);

            goomba = new Goomba(125, 125, 25, 25, assetLoader.loadTexture("Assets/Textures/goomba.png"));
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            text = new SolidBlock(300, 200, 1000, 40, assetLoader.loadFont("Assets/Textures/ariali.ttf",
                String.Format("{0}, {1}", e.X, e.Y), 1000, 40, 20));
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            SwapBuffers();
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, Width, Height, 0, -1, 1);
            this.camera.Zoom = Width * 1.2f / lastWidth;
            lastWidth = Width;
            //shader.loadOrthoMatrix(Matrix4.Mult(Matrix4.Identity, Matrix4.CreateOrthographicOffCenter(0, Width, 0, Height, -1, 1)));
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            
            this.camera.Zoom += -0.001f;
            foreach (Entity entity in this.entities)
            {
                entity.collide(this.player);
                //entity.collide(goomba);
            }
            //this.goomba.update();
            this.player.update();
            this.camera.update();

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            //GL.ClearColor(Color.Black);

            entityRenderer.begin();

            //this.entityRenderer.render(goomba);
            this.entityRenderer.render(player);
            this.entityRenderer.render(text);
            foreach (Entity entity in this.entities) {
                this.entityRenderer.render(entity);
            }


            entityRenderer.end();

            //GL.Flush();
            this.SwapBuffers();

            this.Title = "x: " + player.x.ToString() + ", y:" + player.y.ToString() + " fps: " + RenderFrequency.ToString();

        }
        

    }
}
