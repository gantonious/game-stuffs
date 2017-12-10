using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeatWaveTest.EntityFramework.Components;

namespace HeatWaveTest.EntityFramework.Systems
{
    class CollisionHandler : System
    {
        public override void Process(Entity entity)
        {
            Collide collide = entity.GetComponent<Collide>();
            Player player = entity.GetComponent<Player>();
            SpriteComponent sprite = entity.GetComponent<SpriteComponent>();

            if (collide == null || player == null || sprite == null) return;

            entity.RemoveComponent<Collide>();
            sprite.Sprite.Width *= 1.01f;
            sprite.Sprite.Height *= 1.01f;
        }
    }
}
