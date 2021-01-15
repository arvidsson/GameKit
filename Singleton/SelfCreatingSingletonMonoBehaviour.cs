using UnityEngine;

namespace UnityLib.Singleton
{
    /// <summary>
    /// Self-creating Singleton MonoBehaviour.
    /// </summary>
    public class SelfCreatingSingletonMonoBehaviour<T> : MonoBehaviour where T : SelfCreatingSingletonMonoBehaviour<T>
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
                instance = new GameObject(typeof(T).Name).AddComponent<T>();
            }
            else
            {
                Debug.LogError("Found too many instances (" + instances.Length + ") of singleton " + typeof(T).Name);
            }
        }
    }
}