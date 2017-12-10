using HeatWave.Graphics;

namespace HeatWaveTest.EntityFramework.Components
{
    public class SpriteComponent : Component
    {
        public Sprite Sprite { get; set; }

        public SpriteComponent(Sprite sprite)
        {
            Sprite = sprite;
        }
    }
}
