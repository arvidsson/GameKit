using UnityEngine;

namespace UnityEngine
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Returns a component but checking the result first via an assert.
        /// </summary>
        public static T GetRequiredComponent<T>(this Transform transform)
        {
            T result = transform.GetComponent<T>();

            Debug.Assert(result != null, "Failed to get component: " + typeof(T).ToString(), transform);

            return result;
        }

        /// <summary>
        /// Returns a child by name but first check if the child was found via an assert.
        /// </summary>
        public static Transform FindRequired(this Transform transform, string name)
        {
            Transform t = transform.Find(name);

            Debug.Assert(t != null, "Failed to find child: " + name, transform);

            return t;
        }

        /// <summary>
        /// Returns true if the gameobject has a specific component.
        /// </summary>
        public static bool HasComponent<T>(this Transform transform)
        {
            return (transform.GetComponent<T>() as Component) != null;
        }

        /// <summary>
        /// Destroys all children belonging to the gameobject.
        /// </summary>
        public static void DestroyChildren(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// Destroys all children immediately belonging to the gameobject.
        /// </summary>
        public static void DestroyChildrenImmediately(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                GameObject.DestroyImmediate(child.gameObject);
            }
        }

        /// <summary>
        /// Sets the x position of the transform.
        /// </summary>
        public static void SetX(this Transform transform, float x)
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

        /// <summary>
        /// Sets the y position of the transform.
        /// </summary>
        public static void SetY(this Transform transform, float y)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }

        /// <summary>
        /// Sets the z position of the transform.
        /// </summary>
        public static void SetZ(this Transform transform, float z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }

        /// <summary>
        /// Sets the active state of the gameobject.
        /// </summary>
        public static void SetActive(this Transform transform, bool active)
        {
            transform.gameObject.SetActive(active);
        }

        /// <summary>
        /// Returns all children belonging to the gameobject.
        /// </summary>
        public static Transform[] GetAllChildren(this Transform transform)
        {
            int count = transform.childCount;
            Transform[] children = new Transform[count];

            for (int i = 0; i < count; i++)
            {
                Transform child = transform.GetChild(i);
                children[i] = child;
            }

            return children;
        }

        /// <summary>
        /// Returns the direction vector from the transform's position to a destination position.
        /// </summary>
        public static Vector3 DirectionTo(this Transform transform, Vector3 destination)
        {
            return Vector3.Normalize(destination - transform.position);
        }
    }
}