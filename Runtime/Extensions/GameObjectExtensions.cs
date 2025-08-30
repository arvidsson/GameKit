namespace UnityEngine
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Returns a component but checking the result first via an assert.
        /// </summary>
        public static T GetRequiredComponent<T>(this GameObject gameObject)
        {
            T result = gameObject.GetComponent<T>();

            Debug.Assert(result != null, "Failed to get component: " + typeof(T).ToString(), gameObject);

            return result;
        }

        /// <summary>
        /// Returns true if the gameobject has a specific component.
        /// </summary>
        public static bool HasComponent<T>(this GameObject gameObject)
        {
            return (gameObject.GetComponent<T>() as Component) != null;
        }

        /// <summary>
        /// Returns true if the Component's GameObject has a component of type T.
        /// </summary>
        public static bool HasComponent<T>(this Component component) where T : Component
        {
            return component.GetComponent<T>() != null;
        }

        /// <summary>
        /// Destroys all children belonging to the gameobject.
        /// </summary>
        public static void DestroyChildren(this GameObject gameObject)
        {
            foreach (Transform child in gameObject.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// Destroys all children immediately belonging to the gameobject.
        /// </summary>
        public static void DestroyChildrenImmediately(this GameObject gameObject)
        {
            foreach (Transform child in gameObject.transform)
            {
                GameObject.DestroyImmediate(child.gameObject);
            }
        }

        /// <summary>
        /// Returns all children belonging to the gameobject.
        /// </summary>
        public static GameObject[] GetAllChildren(this GameObject gameObject)
        {
            int count = gameObject.transform.childCount;
            GameObject[] children = new GameObject[count];

            for (int i = 0; i < count; i++)
            {
                Transform child = gameObject.transform.GetChild(i);
                children[i] = child.gameObject;
            }

            return children;
        }

        /// <summary>
        /// Returns a child by name if found, otherwise null.
        /// </summary>
        public static Transform Find(this GameObject gameObject, string name)
        {
            return gameObject.transform.Find(name);
        }

        /// <summary>
        /// Returns a child by name but first check if the child was found via an assert.
        /// </summary>
        public static Transform FindRequired(this GameObject gameObject, string name)
        {
            return gameObject.transform.FindRequired(name);
        }

        /// <summary>
        /// Changes the layer of the GameObject.
        /// </summary>
        public static void SetLayer(this GameObject gameObject, string layer)
        {
            gameObject.layer = LayerMask.NameToLayer(layer);
        }

        /// <summary>
        /// Changes the layer of the GameObject and all its children.
        /// </summary>
        public static void SetLayerChildren(this GameObject gameObject, string layer)
        {
            SetLayerRecursively(gameObject, layer);
        }

        /// <summary>
        /// Toggles the active state of the GameObject.
        /// </summary>
        public static void ToggleActive(this GameObject gameObject)
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        private static void SetLayerRecursively(GameObject gameObject, string layer)
        {
            if (gameObject == null) return;

            gameObject.layer = LayerMask.NameToLayer(layer);

            foreach (Transform child in gameObject.transform)
            {
                if (child == null) continue;
                SetLayerRecursively(child.gameObject, layer);
            }
        }
    }
}