using System.Collections.Generic;

namespace GameKit.State
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

        public void ChangeState<S>() where S : State<T>, new()
        {
            var newType = typeof(S);

            if (currentState?.GetType() == newType)
                return;

            if (!states.ContainsKey(newType))
            {
                AddState(new S());
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

    public class StateMachine
    {
        private readonly Dictionary<System.Type, State> states = new Dictionary<System.Type, State>();
        private State currentState;
        private State nextState;

        public void AddState(State state)
        {
            state.Init(this);
            states[state.GetType()] = state;
        }

        public void ChangeState<S>() where S : State, new()
        {
            var newType = typeof(S);

            if (currentState?.GetType() == newType)
                return;

            if (!states.ContainsKey(newType))
            {
                AddState(new S());
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