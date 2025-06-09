using UnityEngine;

namespace Marx.Utilities {
    public static class GameObjectExtensions {

        /// <summary>
        /// Attempts to find a component of type <typeparamref name="T"/> in the children of the specified GameObject.
        /// </summary>
        /// <typeparam name="T">The type of component to search for.</typeparam>
        /// <param name="gameObject">The GameObject to search in its children.</param>
        /// <param name="component">When this method returns, contains the component of type <typeparamref name="T"/> if found; otherwise, the default value for the type.</param>
        /// <returns>True if a component of type <typeparamref name="T"/> is found in the children of the GameObject; otherwise, false.</returns>
        public static bool TryGetComponentInChildren<T>(this GameObject gameObject, out T component) {
            component = default;
            component = gameObject.GetComponentInChildren<T>();
            return component != null;
        }

        /// <summary>
        /// Attempts to find a component of type <typeparamref name="T"/> in the parent hierarchy of the specified GameObject.
        /// </summary>
        /// <typeparam name="T">The type of component to search for.</typeparam>
        /// <param name="gameObject">The GameObject to search in its parent hierarchy.</param>
        /// <param name="component">When this method returns, contains the component of type <typeparamref name="T"/> if found; otherwise, the default value for the type.</param>
        /// <returns>True if a component of type <typeparamref name="T"/> is found in the parent hierarchy of the GameObject; otherwise, false.</returns>
        public static bool TryGetComponentInParent<T>(this GameObject gameObject, out T component) {
            component = default;
            component = gameObject.GetComponentInParent<T>();
            return component != null;
        }

        /// <summary>
        /// Retrieves a component of type <typeparamref name="T"/> from the specified GameObject. If the component does not exist, it is added to the GameObject.
        /// </summary>
        /// <typeparam name="T">The type of component to retrieve or add.</typeparam>
        /// <param name="gameObject">The GameObject to retrieve the component from or to which the component is added if it does not exist.</param>
        /// <returns>The existing or newly added component of type <typeparamref name="T"/>.</returns>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component {
            T component = gameObject.GetComponent<T>();
            if (component == null) {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }
    }
}