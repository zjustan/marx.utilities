using UnityEngine;

namespace Marx.Utilities
{

    public interface IUnityComponent
    {
        public string name { get; set; }
        public string tag { get; set; }
        public Transform transform { get; }
        public GameObject gameObject { get; }

        public bool enabled { get; set; }
    }

}