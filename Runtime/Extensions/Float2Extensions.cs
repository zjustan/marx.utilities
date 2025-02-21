using Unity.Mathematics;

namespace Marx.Utilities
{
    public static class Float2Extensions
    {
        public static float2 Cross(this float2 v)
        {
            return new float2(-v.y, v.x);
        }

    }
}
