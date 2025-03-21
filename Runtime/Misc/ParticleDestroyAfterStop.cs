using System.Collections;
using UnityEngine;

namespace Marx.Utilities
{
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
