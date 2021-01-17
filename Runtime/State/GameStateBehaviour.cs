using UnityEngine;

namespace UnityLib.State
{
    /// <summary>
    /// A game state. Should be a child to the GameStateManager gameobject.
    /// </summary>
    public abstract class GameStateBehaviour : MonoBehaviour
    {
        private GameStateManager gameStateManager;

        /// <summary>
        /// Sets up everything the game state needs.
        /// </summary>
        public virtual void OnInit() { }

        /// <summary>
        /// Called when the game state is entered.
        /// </summary>
        public virtual void OnEnter() { }

        /// <summary>
        /// Called when the game state is exited.
        /// </summary>
        public virtual void OnExit() { }

        /// <summary>
        /// Called once every frame for the active game state.
        /// </summary>
        public virtual void OnUpdate() { }

        private void Awake()
        {
            gameStateManager = gameObject.GetComponentInParent<GameStateManager>();
            OnInit();
        }

        private void OnEnable()
        {
            OnEnter();
        }

        private void OnDisable()
        {
            OnExit();
        }

        private void Update()
        {
            OnUpdate();
        }
    }
}