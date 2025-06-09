using Unity.Mathematics;

namespace Marx.Utilities {
    public static class Float2Extensions {
        /// <summary>
        /// returns the perpendicular / cross direction of the direction, this vector will be facing to the left side
        /// </summary>
        /// <returns>(-input.y, input.x);</returns>
        public static float2 Perpendicular(this float2 input) {
            return new float2(-input.y, input.x);
        }

        /// <summary>
        /// returns the perpendicular / cross direction of the direction, this vector will be facing to the left side
        /// </summary>
        /// <returns>(-input.y, input.x);</returns>
        public static float2 Random(this float2 input) {
            return UnityEngine.Random.Range(input.x, input.y);
        }

    }
}