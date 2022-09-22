using UnityEngine;

namespace UnityEngine
{
    public static class Vector2Extensions
    {
        /// <summary>
        /// Converts the Vector2 to a Vector2Int.
        /// </summary>
        public static Vector2Int ToVector2Int(this Vector2 v)
        {
            return new Vector2Int((int)v.x, (int)v.y);
        }
        
        /// <summary>
        /// Returns new Vector2 with optionally new x and y values.
        /// </summary>
        public static Vector2 With(this Vector2 v, float? x = null, float? y = null)
        {
            float newX = x ?? v.x;
            float newY = y ?? v.y;
            return new Vector2(newX, newY);
        }
    }
}