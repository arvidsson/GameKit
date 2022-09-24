using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameKit.Pool
{
    /// <summary>
    /// A pool that creates and manages MonoBehaviours on the same GameObject.
    /// </summary>
    public class MonoBehaviourPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        private List<T> pool = new List<T>();

        public T Get()
        {
            var item = pool.Find(x => x.enabled == false);

            if (item == null)
            {
                item = gameObject.AddComponent<T>();
                pool.Add(item);
            }

            return item;
        }

        public void Return(T item)
        {
            item.enabled = false;
        }
    }
}