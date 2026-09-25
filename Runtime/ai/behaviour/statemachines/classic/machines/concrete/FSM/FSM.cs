using System;
using System.Collections.Generic;

namespace SGLib.AI.Behaviour.StateMachines.Classic
{
    /// <summary>
    /// Generic finite state machine (FSM) implementation that manages state transitions.
    /// </summary>
    public class FSM : StateMachineBase<IState>
    {
        public FSM(IState currentState, IEqualityComparer<IState> stateComparer = null)
        {
            CurrentState = currentState ?? throw new ArgumentNullException(nameof(currentState));
            PreviousState = null;
            StateComparer = stateComparer ?? EqualityComparer<IState>.Default;
        }

        public override void Execute(float deltaTime)
        {
            if (!IsRunning) return;

            CurrentState?.Execute(this, deltaTime);
        }

        public override void TransitionTo(IState newState, bool doEnter = true, bool doExit = true, bool allowSameState = false)
        {
            if (newState == null)
                throw new ArgumentNullException(nameof(newState));

            if (!allowSameState && IsSameState(CurrentState, newState))
                return;

            PreviousState = CurrentState;
            if (doExit) CurrentState.Exit();
            CurrentState = newState;
            if (doEnter) CurrentState.Enter();
        }
    }
}

