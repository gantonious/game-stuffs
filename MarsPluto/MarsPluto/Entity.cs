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
    public class CollisonEventArgs : EventArgs
    {   
        private string _message;

        public string message {
            get {return this._message;}
            set {}
        }

        public CollisonEventArgs(string message) {
            this.message = message;
        }
    }

    abstract class Entity
    {
        private float _height;
        private float _width;
        private float _x;
        private float _y;
        private float _xSpeed;
        private float _ySpeed;
        private int _texture;
        protected float u1;
        protected float u2;
        protected float v1;
        protected float v2;
        public TexturedRegion texRegion;
        public event EventHandler<CollisonEventArgs> Collision;

        public float height 
        {
            get { return this._height; }
            set { this._height = value;  }
        }
        public float width
        {
            get { return this._width; }
            set { this._width = value; }
        }
        public float x {
            get { return this._x; }
            set { this._x = value; }
        }
        public float y {
            get { return this._y; }
            set { this._y = value; }
        }
        public float xSpeed
        {
            get { return this._xSpeed; }
            set { this._xSpeed = value; }
        }
        public float ySpeed
        {
            get { return this._ySpeed; }
            set { this._ySpeed = value; }
        }
        public int texture
        {
            get { return this._texture; }
            set { this._texture = value; }
        }

        public float U1
        {
            get { return this.u1; }
        }

        public float U2
        {
            get { return this.u2; }
        }

        public float V1
        {
            get { return this.v1; }
        }

        public float V2
        {
            get { return this.v2; }
        }


        public Entity(float x, float y, float width, float height, int texture)
        {
            this.height = height;
            this.width = width;
            this.x = x;
            this.y = y;
            this.u1 = 0;
            this.u2 = 1;
            this.v1 = 0;
            this.v2 = 1;
            this.xSpeed = 0;
            this.ySpeed = 0;
            this.texture = texture;
            this.Collision += this.onCollideTest;
        }

        abstract public void update();
        abstract public void collide(Entity target);
        abstract public void onCollideTest(object sender, CollisonEventArgs myArgs);
        public void onCollide(object target, CollisonEventArgs myArgs)
        {
            Collision(target, myArgs);
        }
        
    }

    class SolidBlock : Entity
    {
        public SolidBlock(float x, float y, float height, float width, int texture) : base(x, y, height, width, texture) { }

        public override void collide(Entity target)
        {
            if (target.x + target.width > this.x && target.x  < this.x + this.width
                && target.y + target.height > this.y && target.y < this.y + this.height)
            {
                if (target.x < this.x && target.y + target.height > this.y && target.y < this.y + this.height)
                {
                    target.x = this.x - target.width;
                }
                else if (target.x + target.width > this.x + this.width && target.y + target.height > this.y && target.y < this.y + this.height)
                {
                    target.x = this.x + this.width;
                }

                target.xSpeed = (-1) * target.xSpeed;

                onCollide(target, new CollisonEventArgs("buts"));
            }
        }

        public override void onCollideTest(object sender, CollisonEventArgs myArgs)
        {
            Console.WriteLine(sender + "block");
            
        }

        public override void update()
        {
            throw new NotImplementedException();
        }
    }

    class Player : Entity
    {

        private IGameInput input;
        private int lastVert;
        private decimal lastTime;

        public Player(float x, float y, float height, float width, int texture) : base(x, y, height, width, texture) {
            this.input = new MouseAndKeyboard();
            this.texRegion = new TexturedRegion(64 * 3, 256, 64, 64);
            setTexRegion(0, 0);
            lastVert = 0;
            lastTime = DateTime.Now.Ticks / (decimal)TimeSpan.TicksPerMillisecond;
        }

        private void setTexRegion(int column, int row)
        {
            Vector4 region = texRegion.getTexCoords(column, row);
            this.u1 = region[0];
            this.u2 = region[2];
            this.v1 = region[1];
            this.v2 = region[3];
        }

        public override void collide(Entity target)
        {
            throw new NotImplementedException();
        }

        public override void onCollideTest(object sender, CollisonEventArgs myArgs)
        {
            Console.WriteLine("player");
        }

        public override void update()
        {
            this.input.poll();
            if (input.getVertical() < 0)
            {
                if (((DateTime.Now.Ticks / (decimal)TimeSpan.TicksPerMillisecond) - lastTime) > 200)
                {
                    setTexRegion((lastVert % 3) , 1);
                    lastVert += 1;
                    lastTime = DateTime.Now.Ticks / (decimal)TimeSpan.TicksPerMillisecond;
                }

            }
            else if (input.getVertical() > 0)
            {
                if (((DateTime.Now.Ticks / (decimal)TimeSpan.TicksPerMillisecond) - lastTime) > 200)
                {
                    setTexRegion((lastVert % 3), 2);
                    lastVert += 1;
                    lastTime = DateTime.Now.Ticks / (decimal)TimeSpan.TicksPerMillisecond;
                }
            }
            else if (input.getHorizontal() < 0)
            {
                if (((DateTime.Now.Ticks / (decimal)TimeSpan.TicksPerMillisecond) - lastTime) > 200)
                {
                    setTexRegion((lastVert % 3), 0);
                    lastVert += 1;
                    lastTime = DateTime.Now.Ticks / (decimal)TimeSpan.TicksPerMillisecond;
                }

            }
            else if (input.getHorizontal() > 0)
            {
                if (((DateTime.Now.Ticks / (decimal)TimeSpan.TicksPerMillisecond) - lastTime) > 200)
                {
                    setTexRegion((lastVert % 3), 3);
                    lastVert += 1;
                    lastTime = DateTime.Now.Ticks / (decimal)TimeSpan.TicksPerMillisecond;
                }
            }
            else
            {
                setTexRegion(1, 1);
            }
            this.x += this.input.getHorizontal() * 5;
            this.y += this.input.getVertical()*5;
        }
    }

    class Goomba : Entity
    {   
        
        public Goomba(float x, float y, float height, float width, int texture) : base(x, y, height, width, texture) 
        {
            base.xSpeed = 1;
        }

        public override void collide(Entity target)
        {
            throw new NotImplementedException();
        }

        public override void update()
        {
            base.x += base.xSpeed;
        }
        public override void onCollideTest(object sender, CollisonEventArgs myArgs)
        {
            Console.WriteLine("goom");
        }

    }

    
}
