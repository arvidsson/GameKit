using UnityEngine;

namespace GameKit
{
    /// <summary>
    /// Useful math helper methods.
    /// </summary>
    public static class MathHelper
    {
        /// <summary>
		/// Maps a value from some arbitrary range to the 0 to 1 range.
		/// </summary>
		/// <param name="value">Value</param>
		/// <param name="min">Minimum value</param>
		/// <param name="max">Maximum value</param>
		public static float Map01(float value, float min, float max)
        {
            return (value - min) * 1f / (max - min);
        }

        /// <summary>
        /// Maps a value from some arbitrary range to the 1 to 0 range.
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        public static float Map10(float value, float min, float max)
        {
            return 1f - Map01(value, min, max);
        }

        /// <summary>
        /// Maps a value (which is in the range leftMin - leftMax) to a value in the range rightMin - rightMax.
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="leftMin">Left minimum</param>
        /// <param name="leftMax">Left max</param>
        /// <param name="rightMin">Right minimum</param>
        /// <param name="rightMax">Right max</param>
        public static float Map(float value, float leftMin, float leftMax, float rightMin, float rightMax)
        {
            return rightMin + (value - leftMin) * (rightMax - rightMin) / (leftMax - leftMin);
        }

        /// <summary>
        /// Rounds value to the nearest number in steps of roundToNearest. Ex: rounding 127 to nearest 5 results in 125
        /// </summary>
        /// <returns>The to nearest value</returns>
        /// <param name="value">Value</param>
        /// <param name="roundToNearest">Round to nearest</param>
        public static float RoundToNearest(float value, float roundToNearest)
        {
            return Mathf.Round(value / roundToNearest) * roundToNearest;
        }

        /// <summary>
        /// Clamps a rotation around the x axis.
        /// </summary>
        /// <returns></returns>
        public static Quaternion ClampRotationAroundXAxis(Quaternion q, float minViewAngle, float maxViewAngle)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
            angleX = Mathf.Clamp(angleX, minViewAngle, maxViewAngle);
            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

        /// <summary>
        /// Returns a spherical point.
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="beta"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static Vector3 GetSphericalPoint(float alpha, float beta, float radius)
        {
            Vector3 point;
            point.x = radius * Mathf.Sin(alpha) * Mathf.Cos(beta);
            point.y = radius * Mathf.Sin(alpha) * Mathf.Sin(beta);
            point.z = radius * Mathf.Cos(alpha);

            return point;
        }

        /// <summary>
        /// Converts a 1D index to a 2D coordinate and returns the x-coordinate.
        /// </summary>
        public static int IndexToXCoord(int index, int width) => index % width;

        /// <summary>
        /// Converts a 1D index to a 2D coordinate and returns the y-coordinate.
        /// </summary>
        public static int IndexToYCoord(int index, int width) => (int)(index / width);

        /// <summary>
        /// Converts a 1D index to a 2D coordinate.
        /// </summary>
        public static Vector2Int IndexToCoord(int index, int width) => new Vector2Int(IndexToXCoord(index, width), IndexToYCoord(index, width));

        /// <summary>
        /// Converts a 2D coordinate to a 1D index.
        /// </summary>
        public static int CoordToIndex(int x, int y, int width) => y * width + x;

        /// <summary>
        /// Converts a 2D coordinate to a 1D index.
        /// </summary>
        public static int CoordToIndex(Vector2Int position, int width) => CoordToIndex(position.x, position.y, width);
    }
}