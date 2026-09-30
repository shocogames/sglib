using System;
using System.Collections.Generic;

namespace SGLib.AI.Behaviour.StateMachines.Classic
{
    public class FSMPreemptive : FSM
    {
        /// <summary>
        /// Preemptive state that can evaluate whether it should interrupt the current state.
        /// When a preemption occurs, the preemptive state is transitioned into and behaves like any
        /// other state (its <see cref="StateBase.Enter"/>, <see cref="StateBase.Execute"/>,
        /// and <see cref="StateBase.Exit"/> methods are invoked as part of normal state changes).
        /// </summary>
        /// <remarks>
        /// <para>
        /// The <see cref="PreemptiveStateBase.EvaluatePreemption(FSMPreemptive)"/> method
        /// is called every update cycle (if a preemptive state is assigned). This method typically decides
        /// whether to call <see cref="FSM.TransitionTo(IState, bool, bool, bool)"/> to transition into the preemptive
        /// state or another state.
        /// </para>
        /// <para>
        /// This preemptive mechanism is intended for high-priority, interrupting behaviors that may need 
        /// to take over from any other active state.
        /// </para>
        /// </remarks>
        private PreemptiveStateBase preemptiveState;

        /// <summary>
        /// Creates a new preemptive finite state machine.
        /// </summary>
        /// <param name="currentState">The initial active state.</param>
        /// <param name="preemptiveState">Preemptive state</param>
        /// <param name="stateComparer">
        /// Optional comparer used to determine state equality.
        /// If <c>null</c>, <see cref="EqualityComparer{T}.Default"/> is used.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="currentState"/> or <paramref name="preemptiveState"/> is <c>null</c>.
        /// </exception>
        public FSMPreemptive(IState currentState, PreemptiveStateBase preemptiveState, IEqualityComparer<IState> stateComparer = null)
            : base(currentState, stateComparer)
        {
            this.preemptiveState = preemptiveState ?? throw new ArgumentNullException(nameof(preemptiveState));
        }

        /// <summary>
        /// Updates the state machine.
        /// The preemptive state is evaluated first; if it triggers a state change,
        /// the execution of the previous state is skipped for this update cycle.
        /// The current state is updated in second place.
        /// </summary>
        public override void Execute(float deltaTime)
        {
            if (!IsRunning) return;

            var before = CurrentState;

            preemptiveState?.EvaluatePreemption(this);

            if (!IsSameState(before, CurrentState))
                return; // state changed during preemption; skip Execute for this tick

            CurrentState?.Execute(this, deltaTime);
        }

        /// <summary>
        /// Sets or replaces the preemptive state.
        /// </summary>
        /// <param name="newPreempState">
        /// The new preemptive state. Can be <c>null</c> to disable preemption.
        /// </param>
        /// <param name="allowSameState">
        /// If <c>true</c>, allows assigning the same preemptive state again.
        /// </param>
        public void SetPreemptiveState(PreemptiveStateBase newPreempState, bool allowSameState = false)
        {
            if (newPreempState == null)
                throw new ArgumentNullException(nameof(newPreempState));

            if (!allowSameState && IsSameState(preemptiveState, newPreempState))
                return;

            preemptiveState = newPreempState;
        }
    }
}

