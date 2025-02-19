using System.Collections.Generic;
using UnityEngine;

namespace Marx.Utilities
{
    public static class ComponentExtensions
    {
        public static IEnumerable<T> WithComponent<T>(this IEnumerable<Component> components)
        {
            foreach (var component in components)
                if (component.TryGetComponent(out T result))
                    yield return result;
        }

        public static IEnumerable<T> WithComponentInParent<T>(this IEnumerable<Component> components)
        {
            foreach (var component in components)
            {
                if (component.TryGetComponentInParent(out T result))
                    yield return result;
            }
        }

        public static bool TryGetComponentInParent<T>(this Component component, out T result)
        {
            result = component.GetComponentInParent<T>();
            return result != null;
        }
    }
}
