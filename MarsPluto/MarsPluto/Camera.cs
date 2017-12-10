using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
namespace MarsPluto
{
    class Camera
    {
        private GameWindow window;
        private Vector2 position;
        private Entity focus;
        private float border = 10;
        private float zoom = 1;
        private float rotation = 0;

        public Vector2 Position
        {
            get {return this.position; }
            set {this.position = value; }

        }

        public float Zoom
        {
            get { return this.zoom; }
            set { this.zoom = value; }
        }

        public float Rotation
        {
            get { return this.rotation; }
            set { this.rotation = value; }
        }

        public Camera(GameWindow window, Entity focus)
        {
            this.window = window;
            this.focus = focus;
            this.position = new Vector2(0, 0);
        }

        public void update()
        {
            /*
            if (focus.x < this.position.X + border)
            {
                this.position.X -= border;
            }

            if (focus.x + focus.width > this.position.X + this.window.Width - border)
            {
                this.position.X += border;
            }

            if (focus.y < this.position.Y + border)
            {
                this.position.Y -= border;
            }

            if (focus.y + focus.height > this.position.Y + this.window.Height - border)
            {
                this.position.Y += border;
            }*/

            position.X = focus.x + focus.width / 2 - window.Width / 2;
            position.Y = focus.y + focus.height / 2 - window.Height / 2;

            
        }

    }
}
