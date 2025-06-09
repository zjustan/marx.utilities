using System.Collections;
using UnityEngine;

namespace Marx.Utilities
{
    /// <summary>
    /// A utility class responsible for automatically destroying a particle system
    /// game object after it has stopped playing. This is useful for managing
    /// particle system lifecycles and ensuring that inactive particle systems do not
    /// persist in the scene unnecessarily.
    /// </summary>
    public class ParticleDestroyAfterStop : MonoBehaviour
    {
        [SerializeField] private float aliveTime = 10;

        IEnumerator Start()
        {
            yield return new WaitForSeconds(aliveTime);
            Destroy(gameObject);
        }
    }
}
