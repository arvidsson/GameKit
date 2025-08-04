using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace GameKit
{
    /// <summary>
    /// Easy access to useful application functionality, eg application lifecycle, scene management, paths, etc.
    /// </summary>
    public static class ApplicationHelper
    {
        /// <summary>
        /// Is the application quitting? Useful for example for singletons and their OnDestroy methods.
        /// </summary>
        public static bool IsQuitting => quitting;

        private static bool quitting = false;

        [RuntimeInitializeOnLoadMethod]
        static void RunOnStart()
        {
            UnityEngine.Application.quitting += OnApplicationQuitting;
        }

        /// <summary>
        /// Toggles pause/resume of game in editor.
        /// </summary>
        public static void TogglePauseInEditor()
        {
#if UNITY_EDITOR
            EditorApplication.isPaused = !EditorApplication.isPaused;
#endif
        }

        /// <summary>
        /// Pauses game in editor.
        /// </summary>
        public static void PauseEditor()
        {
#if UNITY_EDITOR
            EditorApplication.isPaused = true;
#endif
        }

        /// <summary>
        /// Resumes game in editor.
        /// </summary>
        public static void ResumeEditor()
        {
#if UNITY_EDITOR
            EditorApplication.isPaused = false;
#endif
        }

        /// <summary>
        /// Quits the application.
        /// </summary>
        public static void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit();
#endif
        }

        /// <summary>
        /// Returns true if mouse pointer is over a GUI element.
        /// </summary>
        public static bool IsMouseOverGUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

        /// <summary>
        /// Returns true if mouse pointer is over a GUI object.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsMouseOverGUIObject(GameObject target, bool checkChildren = true)
        {
            if (EventSystem.current == null) return false;

            var pointer = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            var hits = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, hits);

            foreach (var hit in hits)
            {
                if (hit.gameObject == target || (checkChildren && hit.gameObject.transform.IsChildOf(target.transform)))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Loads and switches to another scene
        /// </summary>
        public static void LoadScene(string name)
        {
            SceneManager.LoadScene(name, LoadSceneMode.Single);
        }

        /// <summary>
        /// Loads and adds another scene to the current already loaded scenes
        /// </summary>
        public static void AddScene(string name)
        {
            SceneManager.LoadScene(name, LoadSceneMode.Additive);
        }

        /// <summary>
        /// Loads and switches to another scene asynchronously
        /// </summary>
        public static void LoadSceneAsync(string name)
        {
            SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        }

        /// <summary>
        /// Loads and adds another scene to the current already loaded scenes asynchronously
        /// </summary>
        public static void AddSceneAsync(string name)
        {
            SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        }

        private static void OnApplicationQuitting()
        {
            quitting = true;
        }
    }
}