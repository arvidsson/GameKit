using System;
using System.Collections.Generic;

namespace GameKit
{

    public abstract class EventBase { }
    public abstract class Event : EventBase { }
    public abstract class Event<T> : EventBase { }
    public abstract class Event<T1, T2> : EventBase { }
    public abstract class Event<T1, T2, T3> : EventBase { }

    public static class Events
    {
        private static readonly Dictionary<Type, Delegate> handlers = new();

        public static void Register<TEvent>(Action handler) where TEvent : Event
        {
            var type = typeof(TEvent);
            if (handlers.TryGetValue(type, out var existing))
                handlers[type] = (Action)existing + handler;
            else
                handlers[type] = handler;
        }

        public static void Unregister<TEvent>(Action handler) where TEvent : Event
        {
            var type = typeof(TEvent);
            if (handlers.TryGetValue(type, out var existing))
            {
                var updated = (Action)existing - handler;
                if (updated == null) handlers.Remove(type);
                else handlers[type] = updated;
            }
        }

        public static void Emit<TEvent>() where TEvent : Event
        {
            if (handlers.TryGetValue(typeof(TEvent), out var del))
                ((Action)del)?.Invoke();
        }

        public static void Register<TEvent, T1>(Action<T1> handler) where TEvent : Event<T1>
        {
            var type = typeof(TEvent);
            if (handlers.TryGetValue(type, out var existing))
                handlers[type] = (Action<T1>)existing + handler;
            else
                handlers[type] = handler;
        }

        public static void Unregister<TEvent, T1>(Action<T1> handler) where TEvent : Event<T1>
        {
            var type = typeof(TEvent);
            if (handlers.TryGetValue(type, out var existing))
            {
                var updated = (Action<T1>)existing - handler;
                if (updated == null) handlers.Remove(type);
                else handlers[type] = updated;
            }
        }

        public static void Emit<TEvent, T1>(T1 arg) where TEvent : Event<T1>
        {
            if (handlers.TryGetValue(typeof(TEvent), out var del))
                ((Action<T1>)del)?.Invoke(arg);
        }

        public static void Register<TEvent, T1, T2>(Action<T1, T2> handler) where TEvent : Event<T1, T2>
        {
            var type = typeof(TEvent);
            if (handlers.TryGetValue(type, out var existing))
                handlers[type] = (Action<T1, T2>)existing + handler;
            else
                handlers[type] = handler;
        }

        public static void Unregister<TEvent, T1, T2>(Action<T1, T2> handler) where TEvent : Event<T1, T2>
        {
            var type = typeof(TEvent);
            if (handlers.TryGetValue(type, out var existing))
            {
                var updated = (Action<T1, T2>)existing - handler;
                if (updated == null) handlers.Remove(type);
                else handlers[type] = updated;
            }
        }

        public static void Emit<TEvent, T1, T2>(T1 arg1, T2 arg2) where TEvent : Event<T1, T2>
        {
            if (handlers.TryGetValue(typeof(TEvent), out var del))
                ((Action<T1, T2>)del)?.Invoke(arg1, arg2);
        }

        public static void Register<TEvent, T1, T2, T3>(Action<T1, T2, T3> handler) where TEvent : Event<T1, T2, T3>
        {
            var type = typeof(TEvent);
            if (handlers.TryGetValue(type, out var existing))
                handlers[type] = (Action<T1, T2, T3>)existing + handler;
            else
                handlers[type] = handler;
        }

        public static void Unregister<TEvent, T1, T2, T3>(Action<T1, T2, T3> handler) where TEvent : Event<T1, T2, T3>
        {
            var type = typeof(TEvent);
            if (handlers.TryGetValue(type, out var existing))
            {
                var updated = (Action<T1, T2, T3>)existing - handler;
                if (updated == null) handlers.Remove(type);
                else handlers[type] = updated;
            }
        }

        public static void Emit<TEvent, T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3) where TEvent : Event<T1, T2, T3>
        {
            if (handlers.TryGetValue(typeof(TEvent), out var del))
                ((Action<T1, T2, T3>)del)?.Invoke(arg1, arg2, arg3);
        }
    }

}
