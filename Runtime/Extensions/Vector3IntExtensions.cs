using UnityEngine;

namespace UnityEngine
{
    public static class Vector3IntExtensions
    {
        /// <summary>
        /// Converts the Vector3Int to a Vector2Int.
        /// </summary>
        public static Vector2Int ToVector2Int(this Vector3Int v)
        {
            return new Vector2Int(v.x, v.y);
        }
        
        /// <summary>
        /// Converts the Vector3Int to a Vector3.
        /// </summary>
        public static Vector3 ToVector3(this Vector3Int v)
        {
            return new Vector3(v.x, v.y, v.y);
        }
        
        /// <summary>
        /// Returns new Vector3Int with optionally new x, y and z values.
        /// </summary>
        public static Vector3Int With(this Vector3Int v, int? x = null, int? y = null, int? z = null)
        {
            int newX = x ?? v.x;
            int newY = y ?? v.y;
            int newZ = z ?? v.z;
            return new Vector3Int(newX, newY, newZ);
        }
    }
}