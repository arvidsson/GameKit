using UnityEngine;

namespace GameKit.Pattern
{
    /// <summary>
    /// A Component is a MonoBehaviour that has easy access to a Main MonoBehaviour that can either be on the same GameObject, or in a parent GameObject.
    /// </summary>
    public class Component<T> : MonoBehaviour where T : MonoBehaviour
    {
        public T Main
        {
            get
            {
                if (!main)
                {
                    main = GetComponent<T>();
                }

                if (!main)
                {
                    main = GetComponentInParent<T>();
                }

                return main;
            }
        }

        private T main;
    }
}