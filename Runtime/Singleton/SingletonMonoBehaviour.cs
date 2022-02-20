using UnityEngine;

namespace GameKit.Singleton
{
    /// <summary>
    /// Singleton MonoBehaviour.
    /// </summary>
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        protected static T instance;

        public bool Exists => instance != null;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    UpdateInstance();
                }

                return instance;
            }
        }

        private void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }

        private static void UpdateInstance()
        {
            var instances = FindObjectsOfType<T>();

            if (instances.Length == 1)
            {
                instance = instances[0];
            }
            else if (instances.Length == 0)
            {
                Debug.LogError("Failed to find any instance of singleton " + typeof(T).Name);
            }
            else
            {
                Debug.LogError("Found too many instances (" + instances.Length + ") of singleton " + typeof(T).Name);
            }
        }
    }
}