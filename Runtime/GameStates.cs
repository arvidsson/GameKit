using GameKit.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameKit
{
    public static class GameStates
    {
        static readonly Dictionary<System.Type, GameState> states = new();
        static GameState currentState;

        public static void Add(GameState state)
        {
            states[state.GetType()] = state;
        }

        public static void Change<T>() where T : GameState
        {
            CoroutinesHelper.Run(ChangeStateNextFrame<T>());
        }

        public static void Update()
        {
            currentState?.Update();
        }

        static IEnumerator ChangeStateNextFrame<T>() where T : GameState
        {
            yield return new WaitForEndOfFrame();

            var type = typeof(T);
            if (!states.ContainsKey(type))
            {
                Debug.LogError($"GameStates: GameState {type} has not been added.");
            }

            if (currentState?.GetType() == type)
                yield break;

            currentState?.Exit();
            currentState = states[type];
            currentState.Enter();
        }
    }
}
