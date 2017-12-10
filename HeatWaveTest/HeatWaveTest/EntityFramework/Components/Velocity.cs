namespace HeatWaveTest.EntityFramework.Components
{
    public class Velocity : Component
    {
        public float XVelocity { get; set; }
        public float YVelocity { get; set; }

        public Velocity(float xVelocity, float yVelocity)
        {
            XVelocity = xVelocity;
            YVelocity = yVelocity;
        }
    }
}
