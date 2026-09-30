namespace SGLib.AI.Behaviour.StateMachines.Classic
{
    /// <summary>
    /// Represents a preemptive state that can interrupt the current active state
    /// of the PFSM and take control when its preemption conditions are met.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A <see cref="PreemptiveStateBase"/> is both a decision maker and a regular state.
    /// While it is not the current active state, its
    /// <see cref="EvaluatePreemption(FSMPreemptive)"/> method is evaluated every update
    /// cycle to determine whether a transition into this state (or another state) should occur.
    /// </para>
    /// <para>
    /// When a preemptive state becomes the current active state, it fully participates in the
    /// normal state lifecycle: its <see cref="StateBase.Enter"/>,
    /// <see cref="StateBase.Execute"/>, and <see cref="StateBase.Exit"/> methods are
    /// invoked just like any other state.
    /// </para>
    /// <para>
    /// This pattern is intended for high-priority or interrupting behaviors that must be able
    /// to take control from any other state, such as forced actions, emergency states, or
    /// external interruptions.
    /// </para>
    /// </remarks>
    public abstract class PreemptiveStateBase : StateBase
    {
        /// <summary>
        /// Evaluates whether this state should preempt the currently active state.
        /// </summary>
        /// <param name="fsmp">The finite state machine evaluating the preemption.</param>
        public abstract void EvaluatePreemption(FSMPreemptive fsmp);
    }
}