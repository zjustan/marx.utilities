using UnityEngine;

namespace Marx.Utilities
{
    public static class GameObjectExtensions
    {
        public static bool TryGetComponentInChildren<T>(this GameObject gameObject, out T component)
        {
            component = default;
            component = gameObject.GetComponentInChildren<T>();
            return component != null;
        }
        
        public static bool TryGetComponentInParent<T>(this GameObject gameObject, out T component)
        {
            component = default;
            component = gameObject.GetComponentInParent<T>();
            return component != null;
        }
    }
}