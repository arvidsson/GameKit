using UnityEngine;

namespace UnityEngine
{
    public static class MonoBehaviourExtensions
    {
        /// <summary>
        /// Returns a component but checking the result first via an assert.
        /// </summary>
        public static T GetRequiredComponent<T>(this MonoBehaviour behaviour)
        {
            T result = behaviour.GetComponent<T>();

            Debug.Assert(result != null, "Failed to get component: " + typeof(T).ToString(), behaviour);

            return result;
        }
    }
}