using UnityEngine;
using UnityEngine.AI;

namespace GameKit
{
    /// <summary>
    /// Physics helper methods.
    /// </summary>
    public static class PhysicsHelper
    {
        /// <summary>
        /// Checks if there is a 2D collider at the screen position and returns it, otherwise null.
        /// </summary>
        public static Collider2D GetCollider2DAtPosition(Vector2 screenPosition)
        {
            var ray = Camera.main.ScreenPointToRay(screenPosition);
            var hit = UnityEngine.Physics2D.Raycast(ray.origin, ray.direction);
            return hit.collider;
        }

        /// <summary>
        /// Checks if there is a 2D collider at the mouse position and returns it, otherwise null.
        /// </summary>
        public static Collider2D GetCollider2DAtMousePosition()
        {
            return GetCollider2DAtPosition(Input.mousePosition);
        }

        /// <summary>
        /// Returns true if the target is within a certain distance from the origin.
        /// </summary>
        public static bool IsWithinDistance(Transform origin, Transform target, float distance)
        {
            return Vector3.Distance(origin.position, target.position) < distance;
        }

        /// <summary>
        /// Returns true if the target is in the field of view of the origin.
        /// </summary>
        public static bool InFieldOfView(Transform origin, Transform target, float fieldOfView)
        {
            return Vector3.Angle(target.position - origin.position, origin.forward) <= fieldOfView;
        }

        /// <summary>
        /// Returns true if the target is in the line of sight of the origin.
        /// </summary>
        public static bool InLineOfSight(Transform origin, Transform target, LayerMask mask)
        {
            return !UnityEngine.Physics.Linecast(origin.position, target.position, mask);
        }

        /// <summary>
        /// Returns a random location inside a sphere with a radius placed at the origin.
        /// </summary>
        public static Vector3 GetRandomLocation(Vector3 origin, float radius, int layermask = -1)
        {
            Vector2 t = Random.insideUnitCircle * radius;
            Vector3 randomPoint = new Vector3(origin.x + t.x, origin.y, origin.z + t.y);
            NavMeshHit navHit;
            NavMesh.SamplePosition(randomPoint, out navHit, radius, layermask);
            return navHit.position;
        }

        /// <summary>
        /// Returns a random location where we make sure the y-position is the same as the origin's.
        /// </summary>
        public static Vector3 GetRandomLocationOnSameY(Vector3 origin, float radius, int layermask = -1)
        {
            Vector2 t = Random.insideUnitCircle * radius;
            Vector3 randomPoint = new Vector3(origin.x + t.x, origin.y, origin.z + t.y);
            return randomPoint;
        }
    }
}