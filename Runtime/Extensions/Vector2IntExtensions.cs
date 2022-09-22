using UnityEngine;

namespace UnityEngine
{
    public static class Vector2IntExtensions
    {
        /// <summary>
        /// Converts the Vector2Int to a Vector3Int.
        /// </summary>
        public static Vector3Int ToVector3Int(this Vector2Int v)
        {
            return new Vector3Int(v.x, v.y, 0);
        }
        
        /// <summary>
        /// Converts the Vector2Int to a Vector3.
        /// </summary>
        public static Vector3 ToVector3(this Vector2Int v)
        {
            return new Vector3(v.x, v.y, 0f);
        }

        /// <summary>
        /// Returns new Vector2Int with optionally new x and y values.
        /// </summary>
        public static Vector2Int With(this Vector2Int v, int? x = null, int? y = null)
        {
            int newX = x ?? v.x;
            int newY = y ?? v.y;
            return new Vector2Int(newX, newY);
        }
    }
}