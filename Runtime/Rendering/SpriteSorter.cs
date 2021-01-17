using UnityEngine;
using System.Collections.Generic;

namespace UnityLib.Rendering
{
    /// <summary>
    /// Sorts sprites so they are shown behind or in front of each other depending on their y-coord.
    /// </summary>
    public static class SpriteSorter
    {
        private static List<SpriteRenderer> sprites = new List<SpriteRenderer>();

        /// <summary>
        /// Registers a sprite so it can be sorted.
        /// </summary>
        public static void RegisterSprite(SpriteRenderer sprite)
        {
            sprites.Add(sprite);
        }

        /// <summary>
        /// Deregisters a sprite, which will stop it from being sorted.
        /// </summary>
        public static void UnregisterSprite(SpriteRenderer sprite)
        {
            sprites.Remove(sprite);
        }

        /// <summary>
        /// Sorts all the sprites by updating the sorting order according to the y-coord.
        /// </summary>
        public static void UpdateSprites()
        {
            foreach (var sprite in sprites)
            {
                sprite.sortingOrder = (int)((sprite.bounds.center.y - sprite.bounds.extents.y) * -100);
            }
        }
    }
}