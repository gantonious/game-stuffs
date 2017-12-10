using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using System.Threading.Tasks;

namespace MarsPluto
{
    abstract class EntityRenderer
    {
        private GameWindow window;
        private Camera camera;
        private Shader shader;

        public GameWindow Window
        {
            get { return this.window; }
            set { this.window = value; }
        }

        public Camera Camera
        {
            get { return this.camera; }
            set { this.camera = value; }
        }

        public Shader Shader
        {
            get { return this.shader; }
            set { this.shader = value; } 
        }

        public EntityRenderer(GameWindow window, Camera camera, Shader shader) {
            this.window = window;
            this.camera = camera;
            this.shader = shader;
 
        }

        abstract public void begin();
        abstract public void end();
        abstract public void render(Entity entity);

    }
}
