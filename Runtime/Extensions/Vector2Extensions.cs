using UnityEngine;

namespace Marx.Utilities {
    
    public static class Vector2Extensions {

        /// <summary>
        /// Converts a 2D vector to a 3D vector by adding the specified z-coordinate.
        /// </summary>
        /// <param name="vector">The 2D vector to be extended.</param>
        /// <param name="z">The z-coordinate to be added to the 2D vector.</param>
        /// <returns>A new 3D vector with the x and y components of the original 2D vector, and the specified z-coordinate.</returns>
        public static Vector3 WithZ(this Vector2 vector, float z) {
            return new Vector3(vector.x, vector.y, z);
        }

        /// <summary>
        /// Generates a random float value between the x and y components of the specified 2D vector.
        /// </summary>
        /// <param name="input">The 2D vector where the x component represents the minimum value and the y component represents the maximum value for the random range.</param>
        /// <returns>A random float value between the x and y components of the vector.</returns>
        public static float Random(this Vector2 input) {
            return UnityEngine.Random.Range(input.x, input.y);
        }
    }
}