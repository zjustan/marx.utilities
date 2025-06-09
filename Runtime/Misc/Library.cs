using System.Linq;
using UnityEngine;

namespace Marx.Utilities {

    /// <summary>
    /// Provides a base class for creating libraries as Unity `ScriptableObject` instances, allowing for the management of shared resources and configurations.
    /// This class is generic and should be extended by specific library implementations.
    /// </summary>
    /// <typeparam name="T">The type of the library class inheriting from `Library`. It must derive from `Library<T>`.</typeparam>
    [Service]
    public abstract class Library<T> : ScriptableObject where T : Library<T> {

        public static void Register(ServiceRegister register) {
            register.Bind<T>().AsSingleton().From(x => Instance);
        }
        
        public static T Instance {
            get {
                if (instance) return instance;
                
                instance = Resources.LoadAll<T>(string.Empty).FirstOrDefault();
                if (instance != null) return instance;
                
                instance = CreateInstance<T>();
                Debug.LogWarning($"library of type{typeof(T).Name} could not be loaded. created a default instance.");

                return instance;
            }
        }
        
       [StaticReload] private static T instance = null;
        
    }
}