using UnityEngine;

namespace UnityEngine
{
    public static class Vector3Extensions
    {
        /// <summary>
        /// Converts the Vector3 to a Vector2Int.
        /// </summary>
        public static Vector2Int ToVector2Int(this Vector3 v)
        {
            return new Vector2Int((int)v.x, (int)v.y);
        }

        /// <summary>
        /// Returns new Vector3 with optionally new x, y and z values.
        /// </summary>
        public static Vector3 With(this Vector3 v, float? x = null, float? y = null, float? z = null)
        {
            float newX = x ?? v.x;
            float newY = y ?? v.y;
            float newZ = z ?? v.z;
            return new Vector3(newX, newY, newZ);
        }
    }
}