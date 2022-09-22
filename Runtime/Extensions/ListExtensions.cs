using System.Collections.Generic;

namespace UnityEngine
{
    public static class ListExtensions
    {
        /// <summary>
        /// Shuffles a list randomly.
        /// </summary>
        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            var rng = new System.Random();

            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// Returns the first item in the list.
        /// </summary>
        public static T First<T>(this List<T> list) => list[0];
        
        /// <summary>
        /// Returns the last item in the list.
        /// </summary>
        public static T Last<T>(this List<T> list) => list[list.Count - 1];
    }
}