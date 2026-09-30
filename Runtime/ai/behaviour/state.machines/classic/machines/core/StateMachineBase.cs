using System;
using System.Collections.Generic;

namespace SGLib.AI.Behaviour.StateMachines.Classic
{
    public abstract class StateMachineBase<T> : IStateMachine where T : IState
    {
        protected bool IsRunning;
        protected T CurrentState;
        protected T PreviousState;
        protected IEqualityComparer<IState> StateComparer;

        public abstract void Execute(float deltaTime);

        public abstract void TransitionTo(IState newState, bool doEnter = true, bool doExit = true, bool allowSameState = false);

        public virtual void RevertToPrevious(bool doEnter = false, bool doExit = true, bool allowSameState = false)
        {
            TransitionTo(PreviousState, doEnter, doExit, allowSameState);
        }

        public virtual bool IsSameState(IState s1, IState s2)
        {
            StateComparer ??= EqualityComparer<IState>.Default;
            return StateComparer.Equals(s1, s2);
        }

        public virtual void Run()
        {
            if (IsRunning) return;

            IsRunning = true;

            CurrentState?.Enter();
        }

        public virtual void Stop()
        {
            if (!IsRunning) return;

            IsRunning = false;

            CurrentState?.Exit();
        }

        public virtual void Pause() => IsRunning = false;
        public virtual void Resume() => IsRunning = true;
    }
}
