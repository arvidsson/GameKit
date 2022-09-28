using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameKit
{
    /// <summary>
    /// Singleton that can be persisted between scenes and auto-created if none exists when trying to access it.
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        protected static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    if (ApplicationHelper.IsQuitting)
                    {
                        return null;
                    }

                    FindOrCreateInstance();
                }

                return instance;
            }
        }

        public static bool Exists => instance != null;

        public bool persist = true;

        private bool skipOnLevelWasLoaded;

        private void Awake()
        {
            if (Exists)
            {
                Destroy(gameObject);
                return;
            }

            instance = (T)this;
            if (persist) DontDestroyOnLoad(gameObject);
            OnSingletonAwake();

            skipOnLevelWasLoaded = true;
        }

        private void Start()
        {
            // Start() is only run once, this way we know when we have loaded the first scene or when we are switching to another scene
            skipOnLevelWasLoaded = false;
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
                OnSingletonDestroy();
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (skipOnLevelWasLoaded)
                return;

            if (instance == this)
            {
                OnSceneSwitched();
            }
        }

        private static void FindOrCreateInstance()
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

        protected virtual void OnSingletonAwake() { }
        protected virtual void OnSingletonDestroy() { }
        protected virtual void OnSceneSwitched() { }
    }
}