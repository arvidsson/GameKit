using UnityEngine;

namespace UnityEngine
{
    public static class CanvasExtensions
    {
        /// <summary>
        /// Returns the scaled position so that we can place things correctly in scaled resolutions, mostly used for figuring out the correct mouse position.
        /// </summary>
        public static Vector2 ScreenToScaledPoint(this Canvas canvas, Vector2 position)
        {
            return new Vector2(position.x / canvas.scaleFactor, position.y / canvas.scaleFactor);
        }
    }
}