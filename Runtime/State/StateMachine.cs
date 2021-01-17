using System.Collections.Generic;

namespace UnityLib.State
{
    public class StateMachine<T>
    {
        protected T context;

        private readonly Dictionary<System.Type, State<T>> states = new Dictionary<System.Type, State<T>>();
        private State<T> currentState;
        private State<T> nextState;

        public StateMachine(T context)
        {
            this.context = context;
        }

        public void AddState(State<T> state)
        {
            state.Init(this, context);
            states[state.GetType()] = state;
        }

        public void ChangeState<R>() where R : State<T>, new()
        {
            var newType = typeof(R);

            if (currentState?.GetType() == newType)
                return;

            if (!states.ContainsKey(newType))
            {
                AddState(new R());
            }

            nextState = states[newType];
        }

        public void Update()
        {
            if (nextState != null)
            {
                currentState?.Exit();
                currentState = nextState;
                nextState = null;
                currentState.Enter();
            }

            currentState?.Update();
        }
    }
}