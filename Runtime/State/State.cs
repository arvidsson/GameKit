﻿namespace GameKit.State
{
    public abstract class State<T>
    {
        protected StateMachine<T> stateMachine;
        protected T context;

        public void Init(StateMachine<T> stateMachine, T context)
        {
            this.stateMachine = stateMachine;
            this.context = context;
            Setup();
        }

        public virtual void Setup() { }
        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
    }

    public abstract class State
    {
        protected StateMachine stateMachine;

        public void Init(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
            Setup();
        }

        public virtual void Setup() { }
        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
    }
}