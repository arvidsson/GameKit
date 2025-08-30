using System;
using System.Collections;
using System.Collections.Generic;

namespace GameKit
{
    /// <summary>
    /// Base class for game actions that can be chained together.
    /// </summary>
    public abstract class GameAction
    {
        /// <summary>
        /// Game actions that will happen before the current performing game action.
        /// </summary>
        public List<GameAction> PreReactions { get; private set; } = new();

        /// <summary>
        /// Game actions that will be chained together with the current performing game action (i.e. happen after it has been performed).
        /// </summary>
        public List<GameAction> Reactions { get; private set; } = new();

        /// <summary>
        /// Game actions that will happen after all chained game actions have been performed.
        /// </summary>
        public List<GameAction> PostReactions { get; private set; } = new();

        /// <summary>
        /// The actual performing of the game action, override this to implement logic or skip that if the game action is more like an "event" that other game actions will respond to.
        /// </summary>
        public virtual IEnumerator Perform()
        {
            yield break;
        }

        /// <summary>
        /// Optional predicate to cancel the game action (the main Perform() coroutine above).
        /// </summary>
        public Func<bool> CancelCondition { get; set; }

        /// <summary>
        /// True if the game action was cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Called if game action was cancelled.
        /// </summary>
        public virtual IEnumerator OnCancel()
        {
            yield break;
        }

        /// <summary>
        /// Called after Perform() regardless if game action was cancelled or not.
        /// </summary>
        public virtual IEnumerator Cleanup()
        {
            yield break;
        }
    }
}