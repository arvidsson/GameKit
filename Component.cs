using UnityEngine;

namespace UnityLib
{
    /// <summary>
    /// A Component is a MonoBehaviour that has easy access to a Root MonoBehaviour that can either be on the same GameObject, or in a parent GameObject.
    /// </summary>
    public class Component<T> : MonoBehaviour
    {
        public T Root
        {
            get
            {
                if (!root)
                {
                    root = GetComponent<T>();
                }

                if (!root)
                {
                    root = GetComponentInParent<T>();
                }

                return root;
            }
        }

        private T root;
    }
}