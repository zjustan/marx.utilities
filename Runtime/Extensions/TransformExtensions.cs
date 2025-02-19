using UnityEngine;

namespace Marx.Utilities
{
    public static class TransformExtensions
    {
        public static void DestroyChilderen(this Transform transform)
        {
            foreach(Transform child in transform)
            {
                Object.Destroy(child.gameObject);

            }
        }
    }
}