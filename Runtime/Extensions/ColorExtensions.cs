using UnityEngine;

namespace UnityEngine
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Sets the red component of a color.
        /// </summary>
        public static Color ToRed(this Color color, float value)
        {
            return new Color(value, color.g, color.b, color.a);
        }

        /// <summary>
        /// Sets the green component of a color.
        /// </summary>
        public static Color ToGreen(this Color color, float value)
        {
            return new Color(color.r, value, color.b, color.a);
        }

        /// <summary>
        /// Sets the blue component of a color.
        /// </summary>
        public static Color ToBlue(this Color color, float value)
        {
            return new Color(color.r, color.g, value, color.a);
        }

        /// <summary>
        /// Sets the alpha component of a color.
        /// </summary>
        public static Color ToAlpha(this Color color, float value)
        {
            return new Color(color.r, color.g, color.b, value);
        }
    }
}