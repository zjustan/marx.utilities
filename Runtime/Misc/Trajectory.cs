using UnityEngine;

namespace Marx.Utilities 
{
    public static class Trajectory
    {
        /// <summary>
        /// Calculates the projectile's position at a given time using initial position and velocity.
        /// </summary>
        /// <param name="initialPosition">The initial position of the projectile.</param>
        /// <param name="initialVelocity">The initial velocity of the projectile.</param>
        /// <param name="timeElapsed">The time elapsed since the projectile was launched.</param>
        /// <returns>The new position of the projectile.</returns>
        public static Vector3 Calculate(Vector3 initialPosition, Vector3 initialVelocity, float timeElapsed)
        {
            // Calculate displacement using kinematic equation
            Vector3 displacement = initialVelocity * timeElapsed + 0.5f * Physics.gravity * Mathf.Pow(timeElapsed, 2);

            // Return the new position
            return initialPosition + displacement;
        }

        public static bool Raycast(Vector3 orgin, Vector3 velocity, float distance, out RaycastHit hit, float stepsize = 0.1f, int layerMask = ~0, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
        {
            Vector3 position = orgin;
            Vector3 direction = velocity.normalized;
            float distanceLeft = distance;
            hit = default;

            while (distanceLeft > 0)
            {
                if (Physics.Raycast(position, direction, out hit, stepsize, layerMask, queryTriggerInteraction))
                {
                    return true;
                }
                distanceLeft -= stepsize;
                Vector3 newPosition = Calculate(orgin, velocity, distance - distanceLeft);
                direction = (position - newPosition);
            }

            return false;
        }

        public static void Gizmo(Vector3 orgin, Vector3 velocity, float distance, float stepsize = 0.1f)
        {
            float distanceLeft = distance;
            Vector3 oldPosition = orgin;

            while (distanceLeft > 0)
            {

                distanceLeft -= stepsize;
                Vector3 newPosition = Calculate(orgin, velocity, distance - distanceLeft);

                Gizmos.DrawLine(oldPosition, newPosition);
                oldPosition = newPosition;


            }
        }
    }
}
