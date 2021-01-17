using System.Collections;
using UnityEngine;

namespace UnityLib.Helpers
{
    /// <summary>
    /// Makes it possible for any class to make use of coroutines.
    /// </summary>
    public static class CoroutinesHelper
    {
        private class CoroutineRunner : MonoBehaviour { }

        private static MonoBehaviour runner;

        /// <summary>
        /// Runs a coroutine. Creates the global coroutine runner if there is none.
        /// </summary>
        /// <param name="coroutine">The coroutine you want to run</param>
        /// <returns>The coroutine that was started.</returns>
        public static Coroutine Run(IEnumerator coroutine)
        {
            if (runner == null)
            {
                runner = new GameObject("CoroutineRunner").AddComponent<CoroutineRunner>();
                MonoBehaviour.DontDestroyOnLoad(runner);
            }

            return runner.StartCoroutine(coroutine);
        }
    }
}