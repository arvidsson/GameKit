using UnityEngine;
using System.Collections.Generic;

namespace GameKit.Pool
{
    public static class GameObjectPool
    {
        const int defaultPoolSize = 3;

        private static Dictionary<GameObject, Pool> pools;

        class PoolMember : MonoBehaviour
        {
            public Pool pool;
        }

        class Pool
        {
            private Stack<GameObject> unusedGameObjects;
            private GameObject prefab;
            private int nextId = 1;

            public Pool(GameObject prefab, int initialSize)
            {
                this.prefab = prefab;
                unusedGameObjects = new Stack<GameObject>(initialSize);
            }

            public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent)
            {
                GameObject obj;

                if (unusedGameObjects.Count == 0)
                {
                    obj = GameObject.Instantiate(prefab, position, rotation, parent);
                    obj.name = prefab.name + " (" + (nextId++) + ")";
                    obj.AddComponent<PoolMember>().pool = this;
                }
                else
                {
                    obj = unusedGameObjects.Pop();

                    if (obj == null)
                    {
                        return Spawn(position, rotation, parent);
                    }
                }

                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.transform.parent = parent;
                obj.SetActive(true);
                return obj;
            }

            public void Despawn(GameObject obj)
            {
                obj.SetActive(false);
                unusedGameObjects.Push(obj);
            }
        }

        static public void Preload(GameObject prefab, int size)
        {
            Init(prefab, size);

            GameObject[] objs = new GameObject[size];

            for (int i = 0; i < size; i++)
            {
                objs[i] = Spawn(prefab, Vector3.zero, Quaternion.identity, null);
            }

            for (int i = 0; i < size; i++)
            {
                Despawn(objs[i]);
            }
        }

        static public GameObject Spawn(GameObject prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null)
        {
            Init(prefab);

            return pools[prefab].Spawn(position, rotation, parent);
        }

        static public void Despawn(GameObject obj)
        {
            PoolMember poolMember = obj.GetComponent<PoolMember>();

            if (poolMember == null)
            {
                Debug.Log("Object '" + obj.name + "' wasn't spawned from a pool. Destroying it instead.");
                GameObject.Destroy(obj);
            }
            else
            {
                poolMember.pool.Despawn(obj);
            }
        }

        private static void Init(GameObject prefab = null, int size = defaultPoolSize)
        {
            if (pools == null)
            {
                pools = new Dictionary<GameObject, Pool>();
            }

            if (prefab != null && !pools.ContainsKey(prefab))
            {
                pools[prefab] = new Pool(prefab, size);
            }
        }
    }
}