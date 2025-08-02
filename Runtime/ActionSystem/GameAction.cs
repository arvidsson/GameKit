using System.Collections;
using System.Collections.Generic;

namespace GameKit.ActionSystem
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
    }
}