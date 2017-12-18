using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeatWave.Graphics;
using HeatWaveTest.EntityFramework.Components;

namespace HeatWaveTest.EntityFramework.Systems
{
    public class RenderingSystem : System
    {
        public override ComponentSelector ComponentSelector { get; } = 
            new ComponentSelector(typeof(Position), typeof(SpriteComponent));

        public BoundedBox WindowBounds { get; set; }

        public Renderer Renderer { get; private set; }

        public RenderingSystem(Renderer renderer)
        {
            Renderer = renderer;
        }

        public override void Begin()
        {
            Renderer.Begin();
            WindowBounds = Engine.GetTaggedEntity("window").GetComponent<BoundedBox>();
        }

        public override void End()
        {
            Renderer.End();
        }

        public override void Process(Entity entity)
        {
            Position position = entity.GetComponent<Position>();
            SpriteComponent sprite = entity.GetComponent<SpriteComponent>();

            if (position.X < WindowBounds.Width && position.Y < WindowBounds.Height)
            {
                Renderer.Draw(position.X, position.Y, sprite.Sprite.Width, sprite.Sprite.Height, sprite.Sprite.Texture);
            }
        }
    }
}
