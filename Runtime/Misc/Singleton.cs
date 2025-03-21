using UnityEngine;

namespace Marx.Utilities
{
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