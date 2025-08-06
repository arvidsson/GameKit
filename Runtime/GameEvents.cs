using System;
using System.Collections.Generic;

namespace GameKit
{

    public abstract class GameEvent { }

    public static class GameEvents
    {
        private static readonly Dictionary<Type, Delegate> handlers = new();

        public static void AddListener<T>(Action<T> handler) where T : GameEvent
        {
            var type = typeof(T);
            if (handlers.TryGetValue(type, out var existing))
                handlers[type] = (Action<T>)existing + handler;
            else
                handlers[type] = handler;
        }

        public static void RemoveListener<T>(Action<T> handler) where T : GameEvent
        {
            var type = typeof(T);
            if (handlers.TryGetValue(type, out var existing))
            {
                var updated = (Action<T>)existing - handler;
                if (updated == null) handlers.Remove(type);
                else handlers[type] = updated;
            }
        }

        public static void Trigger<T>(T args) where T : GameEvent
        {
            if (handlers.TryGetValue(typeof(T), out var handler))
                ((Action<T>)handler)?.Invoke(args);
        }
    }

}
