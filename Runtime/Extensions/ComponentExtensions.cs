using System.Collections.Generic;
using UnityEngine;

namespace Marx.Utilities {
    public static class ComponentExtensions {
        
        /// <summary>
        /// Filters the components in the provided enumerable, returning only those that have a specified component type.
        /// </summary>
        /// <typeparam name="T">The type of the component to filter for.</typeparam>
        /// <param name="components">The collection of components to search through.</param>
        /// <returns>An enumerable of components that contain the specified type.</returns>
        public static IEnumerable<T> WithComponent<T>(this IEnumerable<Component> components) {
            foreach (var component in components)
                if (component.TryGetComponent(out T result))
                    yield return result;
        }

        /// <summary>
        /// Filters the components in the provided enumerable, returning only those that have a specified component type in their parent hierarchy.
        /// </summary>
        /// <typeparam name="T">The type of the component to filter for.</typeparam>
        /// <param name="components">The collection of components to search through.</param>
        /// <returns>An enumerable of components whose parent hierarchy contains the specified type.</returns>
        public static IEnumerable<T> WithComponentInParent<T>(this IEnumerable<Component> components) {
            foreach (var component in components) {
                if (component.TryGetComponentInParent(out T result)) yield return result;
            }
        }

        /// <summary>
        /// Attempts to retrieve a component of a specified type from the component's parent or ancestor objects.
        /// </summary>
        /// <typeparam name="T">The type of the component to retrieve.</typeparam>
        /// <param name="component">The component from which to begin searching for the specified type in its parent hierarchy.</param>
        /// <param name="result">When the method returns, contains the component of the specified type if found; otherwise, contains the default value for the type.</param>
        /// <returns>True if a component of the specified type is found in the parent hierarchy; otherwise, false.</returns>
        public static bool TryGetComponentInParent<T>(this Component component, out T result) {
            result = component ? component.GetComponentInParent<T>() : default;
            return result != null;
        }
    }
}