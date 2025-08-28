namespace Lunar.Components
{
    public struct PositionComponent
    {
        public PositionComponent(float x, float y)
        {
            X = x;
            Y = y;
            Z = 0;
        }

        public PositionComponent(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X, Y, Z;
    }
}