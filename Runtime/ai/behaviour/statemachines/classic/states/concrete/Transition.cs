using System;

namespace SGLib.AI.Behaviour.StateMachines.Classic
{
    public class Transition
    {
        public PriorityHierarchicalStateBase NextState { get; private set; }
        public Transition() { }
        public Transition(PriorityHierarchicalStateBase nextState) => SetNextState(nextState);

        public void SetNextState(PriorityHierarchicalStateBase nextState)
        {
            if (NextState != null) throw new InvalidOperationException(
                "(invalid) NextState is already assigned.");

            NextState = nextState ?? throw new ArgumentNullException(nameof(nextState));
        }

        public bool EnterCondition()
        {
            if (NextState == null) throw new ArgumentNullException(nameof(NextState));
            return NextState.EnterCondition;
        }
    }
}
