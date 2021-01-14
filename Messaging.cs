using System;

namespace UnityLib
{
    /// <summary>
    /// Simple messaging system.
    /// </summary>
    public static class Messaging<T> where T : Delegate
    {
        /// <summary>
        /// Registers a callback.
        /// </summary>
        /// <example>
        /// delegate void MyMessage(int x, int y);
        /// Messaging<MyMessage>.Register(OnMyMessage);
        /// void OnMyMessage(int x, int y) {...}
        /// </example>
        public static void Register(T callback) => Trigger = Delegate.Combine(Trigger, callback) as T;

        /// <summary>
        /// Deregisters a callback.
        /// </summary>

        public static void Unregister(T callback) => Trigger = Delegate.Remove(Trigger, callback) as T;

        /// <summary>
        /// Trigger that is used to send a message.
        /// </summary>
        /// <example>
        /// Messaging<MyMessage>.Trigger?.Invoke(x, y);
        /// </example>
        public static T Trigger { get; private set; }
    }
}