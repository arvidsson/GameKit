using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameKit.Singleton
{
    /// <summary>
    /// Persistent Singleton MonoBehaviour that exists after loading a new scene.
    /// </summary>
    public class PersistentSingletonMonoBehaviour<T> : MonoBehaviour where T : PersistentSingletonMonoBehaviour<T>
    {
        protected static T instance;

        public static T Instance => instance;

        public bool Exists => instance != null;

        private bool skipOnLevelWasLoaded;

        private void Awake()
        {
            if (Exists)
            {
                Destroy(gameObject);
                return;
            }

            instance = (T)this;
            DontDestroyOnLoad(gameObject);
            OnSingletonAwake();
            OnAwakeOrSwitch();

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

        protected virtual void OnSingletonAwake() { }
        protected virtual void OnSingletonDestroy() { }
        protected virtual void OnSceneSwitched() { }
        protected virtual void OnAwakeOrSwitch() { }
    }
}