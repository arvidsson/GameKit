using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace UnityLib
{
    /// <summary>
    /// A reference to a pooled object, which is automatically returned to the pool when disposed.
    /// Returned by ObjectPool<T>.GetRef()
    /// </summary>
    public struct PoolRef<T> : IDisposable where T : new()
    {
        public T Value { get; set; }

        public void Dispose()
        {
            if (Value is IList list)
                list.Clear();
            if (Value is IDictionary dict)
                dict.Clear();
            if (Value is HashSet<string> set)
                set.Clear();

            ObjectPool<T>.Return(Value);
        }
    }

    /// <summary>
    /// An object pool that grows as you use it, returning either a recycled object or a new object.
    /// Useful to use when garbage collection is a concern.
    /// </summary>
    public static class ObjectPool<T> where T : new()
    {
        static ObjectPool()
        {
            pool = new ConcurrentQueue<T>();
        }

        private static readonly ConcurrentQueue<T> pool;

        /// <summary>
        /// Returns a pooled object or a new object if the pool is empty.
        /// </summary>
        public static T Get()
        {
            if (pool.TryDequeue(out T result))
            {
                return result;
            }

            return new T();
        }

        /// <summary>
        /// Returns an object to the pool.
        /// </summary>
        public static void Return(T obj)
        {
            pool.Enqueue(obj);
        }

        /// <summary>
        /// Returns a reference to a pooled object.
        /// </summary>
        public static PoolRef<T> GetRef()
        {
            if (!pool.TryDequeue(out T result))
            {
                result = new T();
            }

            return new PoolRef<T>() { Value = result };
        }
    }
}