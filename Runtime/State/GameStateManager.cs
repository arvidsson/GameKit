using System.Collections;
using UnityEngine;

namespace GameKit.State
{
    /// <summary>
    /// A game state manager that can change the game state.
    /// </summary>
    public class GameStateManager : MonoBehaviour
    {
        private GameStateBehaviour currentState;

        /// <summary>
        /// Changes the active game state to another game state. The switch happens at the end of the frame.
        /// </summary>
        public void ChangeState<T>() where T : GameStateBehaviour
        {
            if (currentState?.GetType() == typeof(T)) return;

            var nextState = GetComponentInChildren<T>(true);
            Debug.Assert(nextState != null);

            StartCoroutine(ChangeStateCoroutine(nextState));
        }

        private IEnumerator ChangeStateCoroutine<T>(T nextState) where T : GameStateBehaviour
        {
            yield return new WaitForEndOfFrame();
            currentState?.gameObject.SetActive(false);
            currentState = nextState;
            currentState.gameObject.SetActive(true);
        }
    }
}