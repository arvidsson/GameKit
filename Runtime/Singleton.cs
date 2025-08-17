using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameKit
{
    /// <summary>
    /// Singleton that 
    /// - is destroyed when the application quits
    /// - can be persisted between scenes
    /// - is auto-created if none exists when trying to access it
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    if (applicationIsQuitting)
                    {
                        return null;
                    }

                    FindOrCreateInstance();
                }

                return instance;
            }
        }

        public static bool Exists => instance != null;

        private static T instance;

        private static bool applicationIsQuitting;

        [SerializeField] bool persist;

        private bool skipOnLevelWasLoaded;

        private void Awake()
        {
            if (Exists)
            {
                Destroy(gameObject);
                return;
            }

            instance = (T)this;

            if (persist)
            {
                DontDestroyOnLoad(gameObject);
            }

            skipOnLevelWasLoaded = true;
            OnSingletonAwake();
        }

        private void Start()
        {
            // Start() is only run once, this way we know when we have loaded the first scene or when we are switching to another scene
            skipOnLevelWasLoaded = false;
            OnSingletonStart();
        }

        private void OnApplicationQuit()
        {
            applicationIsQuitting = true;
            instance = null;
            Destroy(gameObject);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            OnSingletonEnable();
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            OnSingletonDisable();
        }

        private void OnDestroy()
        {
            OnSingletonDestroy();
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
            var instances = FindObjectsByType<T>(FindObjectsSortMode.None);

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
        protected virtual void OnSingletonEnable() { }
        protected virtual void OnSingletonDisable() { }
        protected virtual void OnSingletonStart() { }
        protected virtual void OnSingletonDestroy() { }
        protected virtual void OnSceneSwitched() { }
    }
}

