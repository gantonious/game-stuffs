using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeatWaveTest.EntityFramework.Components;

namespace HeatWaveTest.EntityFramework.Systems
{
    class CollisionSystem : System
    {
        private List<Entity> entities;
        public override void Begin()
        {
            base.Begin();
            entities = new List<Entity>();
        }

        public override void End()
        {
            base.End();
            foreach (Entity entity1 in entities)
            {
                foreach (Entity entity2 in entities)
                {
                    if (entity1 == entity2) continue;
                    Position pos1 = entity1.GetComponent<Position>();
                    SpriteComponent sprite1 = entity1.GetComponent<SpriteComponent>();

                    Position pos2 = entity2.GetComponent<Position>();
                    SpriteComponent sprite2 = entity2.GetComponent<SpriteComponent>();

                    if (sprite1 == null || sprite2 == null) continue;

                    if (pos1.X + sprite1.Sprite.Width > pos2.X  &&
                        pos1.X < pos2.X + sprite2.Sprite.Width &&
                        pos1.Y + sprite1.Sprite.Height > pos2.Y &&
                        pos1.Y < pos2.Y + sprite2.Sprite.Height)
                    {
                        entity1.AddComponent(new Collide());
                        entity2.AddComponent(new Collide());
                        Console.WriteLine("COLLIDE");
                    }

                }
            }
        }

        public override void Process(Entity entity)
        {
            entities.Add(entity);
        }
    }
}
