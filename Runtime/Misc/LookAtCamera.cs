using UnityEngine;

namespace Marx.Utilities
{
    /// <summary>
    /// A MonoBehaviour class that adjusts the GameObject's rotation
    /// to always face a specific camera or target.
    /// </summary>
    public class LookAtCamera : MonoBehaviour
    {
        [SerializeField] private Transform objectToRotate;

        private void Awake()
        {
            if (objectToRotate == null)
                objectToRotate = transform;
        }

        private void Update()
        {
            LookAtOneAxis(Camera.main.transform);
        }

        private void LookAtOneAxis(Transform target)
        {
            var lookPos = transform.position - target.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            objectToRotate.transform.rotation = rotation;
        }
    }
}
