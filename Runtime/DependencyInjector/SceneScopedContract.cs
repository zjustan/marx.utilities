using UnityEngine;
using UnityEngine.SceneManagement;

namespace Marx.Utilities
{
    public class SceneScopedContract : IContract
    {
        private Scene? scope = null;
    
        public bool IsValidOn(object target)
        {
            Scene? targetScene = null;
            if (target is Component component)
            {
                targetScene = component.gameObject.scene;
            }
        
            return targetScene == scope;
        }

        public void Setup(object target)
        {
            if (target is Component component)
            {
                scope = component.gameObject.scene;
            }
        }
    }
}