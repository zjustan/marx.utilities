using UnityEngine;

namespace Marx.Utilities
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Destroys all child GameObjects of the given Transform.
        /// </summary>
        /// <param name="transform">The parent Transform whose children will be destroyed.</param>
        public static void DestroyChilderen(this Transform transform) {
            foreach(Transform child in transform)
            {
                Object.Destroy(child.gameObject);

            }
        }
    }
}