using System.Collections.Generic;
using UnityEngine;

namespace GameKit.Pool
{
    public static class Pool
    {
        const int defaultPoolSize = 8;
    
        class InternalPool
        {
            int nextId = 1;
            readonly Stack<GameObject> inactive;
            internal readonly HashSet<int> activeIds;
            readonly GameObject prefab;
            GameObject parent;
    
            public InternalPool(GameObject prefab, int initialCapacity)
            {
                this.prefab = prefab;
                inactive = new Stack<GameObject>(initialCapacity);
                activeIds = new HashSet<int>();
                parent = new GameObject(prefab.name + "_pool");
            }
    
            public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent = null)
            {
                GameObject go;
    
                if (inactive.Count == 0)
                {
                    go = GameObject.Instantiate<GameObject>(prefab, position, rotation);
                    go.name = prefab.name + "_" + (nextId++);
                    activeIds.Add(go.GetInstanceID());
                }
                else
                {
                    go = inactive.Pop();
                    if (go == null)
                    {
                        return Spawn(position, rotation, parent);
                    }
                }
                go.transform.SetParent(parent, false);
                go.transform.position = position;
                go.transform.rotation = rotation;
                go.SetActive(true);
                return go;
            }
    
            public void Despawn(GameObject go)
            {
                if (go.activeInHierarchy)
                {
                    go.SetActive(false);
                    go.transform.SetParent(parent.transform, false);
                    inactive.Push(go);
                }
            }
        }
    
        static Dictionary<int, InternalPool> pools;
    
        static void Init(GameObject prefab = null, int capacity = defaultPoolSize)
        {
            if (pools == null)
                pools = new Dictionary<int, InternalPool>();
    
            if (prefab != null)
            {
                var prefabID = prefab.GetInstanceID();
                if (!pools.ContainsKey(prefabID))
                    pools[prefabID] = new InternalPool(prefab, capacity);
            }
        }
    
        static public void Preload(GameObject prefab, int capacity = 1)
        {
            Init(prefab, capacity);
    
            var gameObjects = new GameObject[capacity];
    
            for (int i = 0; i < capacity; i++)
            {
                gameObjects[i] = Spawn(prefab, Vector3.zero, Quaternion.identity);
            }
    
            for (int i = 0; i < capacity; i++)
            {
                Despawn(gameObjects[i]);
            }
        }
    
        static public GameObject Spawn(GameObject prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null)
        {
            Init(prefab);
            return pools[prefab.GetInstanceID()].Spawn(position, rotation, parent);
        }
    
        static public T Spawn<T>(GameObject prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null)
        {
            Init(prefab);
            var go = pools[prefab.GetInstanceID()].Spawn(position, rotation, parent);
            return go.GetComponent<T>();
        }
    
        static public void Despawn(GameObject go)
        {
            InternalPool p = null;
    
            foreach (var pool in pools.Values)
            {
                if (pool.activeIds.Contains(go.GetInstanceID()))
                {
                    p = pool;
                    break;
                }
            }
    
            if (p == null)
            {
                Debug.LogWarning("GameObject '" + go.name + "' wasn't spawned from a pool. Destroying it instead.");
                GameObject.Destroy(go);
            }
            else
            {
                p.Despawn(go);
            }
        }
    }
    
    public static class PoolExtensions
    {
        public static void Despawn(this GameObject go)
        {
            Pool.Despawn(go);
        }
    }
}
