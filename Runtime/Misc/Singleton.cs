using UnityEngine;

namespace Marx.Utilities
{
    /// <summary>
    /// Provides a generic implementation of the Singleton design pattern for MonoBehaviour-derived classes.
    /// Ensures a single instance of the specified type that persists across the lifetime of the application.
    /// </summary>
    /// <typeparam name="T">The type of the derived singleton class.</typeparam>
    /// <remarks>
    /// This class automatically manages the initialization and persistence of the singleton instance. If the instance
    /// does not exist, it will create a new GameObject and attach the singleton component to it. The object is tagged
    /// to persist across scene loads, and the singleton instance can be accessed through the static <see cref="Instance"/> property.
    /// </remarks>
    [Service]
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static void Register(ServiceRegister register)
        {
            register.Bind<T>().AsSingleton().From(x => Instance);
        }

        public static T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                
                _instance = FindFirstObjectByType<T>();
                
                if (_instance != null)
                    return _instance;
                
                GameObject obj = new GameObject(typeof(T).Name);
                _instance = obj.AddComponent<T>();
                DontDestroyOnLoad(obj);

                return _instance;
            }
        }

        private static T _instance;
    }
    
}