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
        public ComponentSelector ComponentSelector = new ComponentSelector(typeof(Collide), typeof(Player), typeof(SpriteComponent));

        public override void Process(Entity entity)
        {
            if (entity.HasComponentsFor(ComponentSelector))
            {
                Collide collide = entity.GetComponent<Collide>();
                Player player = entity.GetComponent<Player>();
                SpriteComponent sprite = entity.GetComponent<SpriteComponent>();

                entity.RemoveComponent<Collide>();
                sprite.Sprite.Width *= 1.01f;
                sprite.Sprite.Height *= 1.01f;
            }
        }
    }
}
