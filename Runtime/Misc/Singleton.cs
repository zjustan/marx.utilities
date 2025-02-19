using UnityEngine;

namespace Marx.Utilities 
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {

        public static T Instance
        {
            get
            {
                if(_instance == null)
                    _instance = FindObjectOfType<T>();

                return _instance;
            }
        }

        private static T _instance;
    }
}
