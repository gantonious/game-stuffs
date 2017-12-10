namespace HeatWaveTest.EntityFramework.Components
{
    public class Position : Component
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Position(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
