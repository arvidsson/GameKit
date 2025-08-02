namespace UnityEngine
{
    /// <summary>
    /// Used to store a snapshot of transform data, which can be restored via extension method.
    /// </summary>
    public readonly struct TransformSnapshot
    {
        public readonly Vector3 Position;
        public readonly Vector3 Scale;
        public readonly Quaternion Rotation;
        public readonly int SiblingIndex;

        public TransformSnapshot(Vector3 position, Vector3 scale, Quaternion rotation, int siblingIndex)
        {
            Position = position;
            Scale = scale;
            Rotation = rotation;
            SiblingIndex = siblingIndex;
        }
    }

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

        /// <summary>
        /// Takes a snapshot of the transform's position, scale, rotation, and sibling index.
        /// </summary>
        public static TransformSnapshot TakeSnapshot(this Transform transform)
        {
            return new TransformSnapshot(
                transform.position,
                transform.localScale,
                transform.localRotation,
                transform.GetSiblingIndex()
            );
        }

        /// <summary>
        /// Restores the transform's position, scale, rotation, and sibling index to the values stored in the snapshot.
        /// </summary>
        public static void Restore(this Transform transform, TransformSnapshot snapshot)
        {
            transform.position = snapshot.Position;
            transform.localScale = snapshot.Scale;
            transform.localRotation = snapshot.Rotation;
            transform.SetSiblingIndex(snapshot.SiblingIndex);
        }

        /// <summary>
        /// Restores the transform's position to the value stored in the snapshot.
        /// </summary>
        public static void RestorePosition(this Transform transform, TransformSnapshot snapshot)
        {
            transform.position = snapshot.Position;
        }

        /// <summary>
        /// Restores the transform's scale to the value stored in the snapshot.
        /// </summary>
        public static void RestoreScale(this Transform transform, TransformSnapshot snapshot)
        {
            transform.localScale = snapshot.Scale;
        }

        /// <summary>
        /// Restores the transform's rotation to the value stored in the snapshot.
        /// </summary>
        public static void RestoreRotation(this Transform transform, TransformSnapshot snapshot)
        {
            transform.localRotation = snapshot.Rotation;
        }

        /// <summary>
        /// Restores the transform's sibling index to the value stored in the snapshot.
        /// </summary>
        public static void RestoreSiblingIndex(this Transform transform, TransformSnapshot snapshot)
        {
            transform.SetSiblingIndex(snapshot.SiblingIndex);
        }
    }
}