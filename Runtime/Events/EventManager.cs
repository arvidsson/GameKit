using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityLib.Events
{
    /// <summary>
    /// Simple event manager to handle listeners and sending events to listeners.
    /// </summary>
    public class EventManager
    {
        private readonly Dictionary<Type, IList> listeners = new Dictionary<Type, IList>();

        public void SendEvent<T>(T ev)
        {
            Type t = typeof(T);

            if (listeners.ContainsKey(t))
            {
                List<Action<T>> subscriptions = listeners[t] as List<Action<T>>;

                foreach (var subscription in subscriptions)
                {
                    subscription.Invoke(ev);
                }
            }
        }

        public void AddListener<T>(Action<T> listener)
        {
            Type t = typeof(T);

            if (!listeners.ContainsKey(t))
            {
                listeners.Add(t, new List<Action<T>>());
            }

            listeners[t].Add(listener);
        }

        public void RemoveListener<T>(Action<T> listener)
        {
            Type t = typeof(T);

            if (listeners.ContainsKey(t))
            {
                listeners[t].Remove(listener);
            }
        }
    }
}