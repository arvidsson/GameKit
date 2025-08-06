using System;
using System.Collections;
using System.Collections.Generic;

namespace GameKit
{
    /// <summary>
    /// Singleton that manages all things related to game actions.
    /// </summary>
    public static class GameActions
    {
        /// <summary>
        /// Tells us if a game action is currently being performed, useful if we don't want to allow for example interactions during a game action.
        /// </summary>
        public static bool IsPerforming { get; private set; }

        // subscribers to game actions that either want to add actions before or after a game action
        static Dictionary<Type, List<Action<GameAction>>> preSubs = new();
        static Dictionary<Type, List<Action<GameAction>>> postSubs = new();

        // reactions currently being handled
        static List<GameAction> reactions = null;

        /// <summary>
        /// Adds a prereaction subscriber to a game action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reaction"></param>
        public static void SubscribePrereaction<T>(Action<T> reaction) where T : GameAction
        {
            SubReaction(reaction, preSubs);
        }

        /// <summary>
        /// Adds a postreaction subscriber to a game action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reaction"></param>
        public static void SubscribePostreaction<T>(Action<T> reaction) where T : GameAction
        {
            SubReaction(reaction, postSubs);
        }

        /// <summary>
        /// Removes a prereaction subscriber to a game action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reaction"></param>
        public static void UnsubscribePrereaction<T>(Action<T> reaction) where T : GameAction
        {
            UnsubReaction(reaction, preSubs);
        }

        /// <summary>
        /// Removes a postreaction subscriber to a game action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reaction"></param>
        public static void UnsubscribePostreaction<T>(Action<T> reaction) where T : GameAction
        {
            UnsubReaction(reaction, postSubs);
        }

        /// <summary>
        /// Performs a game action.
        /// 
        /// The performing consists of:
        /// - all prereaction subscribers are called, who may add prereaction game actions
        /// - all prereaction game actions are performed
        /// - the game action is performed (which might lead to reaction game actions being added)
        /// - all reaction game actions are performed
        /// - all postreaction subscribers are called, who may add postreaction game actions
        /// - all postreaction game actions are performed
        /// - onPerformFinished() is called
        /// </summary>
        /// <param name="action"></param>
        /// <param name="onPerformFinished"></param>
        public static void Perform(GameAction action, Action onPerformFinished = null)
        {
            if (IsPerforming) return;
            IsPerforming = true;
            Coroutines.Run(Flow(action, () =>
            {
                IsPerforming = false;
                onPerformFinished?.Invoke();
            }));
        }

        /// <summary>
        /// Adds a game action chained to the currently performed game action.
        /// </summary>
        /// <param name="action"></param>
        public static void AddReaction(GameAction action)
        {
            reactions?.Add(action);
        }

        /// <summary>
        /// Adds game actions chained to the currently performed game action.
        /// </summary>
        /// <param name="actions"></param>
        public static void AddReactions(params GameAction[] actions)
        {
            foreach (var action in actions)
            {
                AddReaction(action);
            }
        }

        static void SubReaction<T>(Action<T> reaction, Dictionary<Type, List<Action<GameAction>>> subs) where T : GameAction
        {
            Type type = typeof(T);
            void wrappedReaction(GameAction action) => reaction((T)action);
            if (subs.ContainsKey(type))
            {
                subs[type].Add(wrappedReaction);
            }
            else
            {
                subs.Add(type, new());
                subs[type].Add(wrappedReaction);
            }
        }

        static void UnsubReaction<T>(Action<T> reaction, Dictionary<Type, List<Action<GameAction>>> subs) where T : GameAction
        {
            Type type = typeof(T);
            if (subs.ContainsKey(type))
            {
                void wrappedReaction(GameAction action) => reaction((T)action);
                subs[type].Remove(wrappedReaction);
            }
        }

        static IEnumerator Flow(GameAction action, Action onFlowFinished = null)
        {
            reactions = action.PreReactions;
            PerformSubscribers(action, preSubs);
            yield return PerformReactions();

            reactions = action.Reactions;
            yield return action.Perform();
            yield return PerformReactions();

            reactions = action.PostReactions;
            PerformSubscribers(action, postSubs);
            yield return PerformReactions();

            onFlowFinished?.Invoke();
        }

        static void PerformSubscribers(GameAction action, Dictionary<Type, List<Action<GameAction>>> subs)
        {
            Type type = action.GetType();
            if (subs.ContainsKey(type))
            {
                foreach (var sub in subs[type])
                {
                    sub(action);
                }
            }
        }

        static IEnumerator PerformReactions()
        {
            foreach (var reaction in reactions)
            {
                yield return Flow(reaction);
            }
        }
    }
}