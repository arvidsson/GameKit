using UnityEngine;

namespace UnityLib
{
    public static class Direction
    {
        public static Vector2Int Up => new Vector2Int(0, 1);
        public static Vector2Int UpRight => new Vector2Int(1, 1);
        public static Vector2Int Right => new Vector2Int(1, 0);
        public static Vector2Int DownRight => new Vector2Int(1, -1);
        public static Vector2Int Down => new Vector2Int(0, -1);
        public static Vector2Int DownLeft => new Vector2Int(-1, -1);
        public static Vector2Int Left => new Vector2Int(-1, 0);
        public static Vector2Int UpLeft => new Vector2Int(-1, 1);

        public static readonly Vector2Int[] Cardinal = new[]
        {
            Up,
            Right,
            Down,
            Left
        };

        public static readonly Vector2Int[] Diagonal = new[]
        {
            UpRight,
            DownRight,
            DownLeft,
            UpLeft
        };

        public static readonly Vector2Int[] All = new[]
        {
            Up,
            UpRight,
            Right,
            DownRight,
            Down,
            DownLeft,
            Left,
            UpLeft
        };
    }
}