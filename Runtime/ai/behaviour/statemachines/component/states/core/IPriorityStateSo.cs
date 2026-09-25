using SGLib.AI.Behaviour.StateMachines.Component.PriorityComparers;

namespace SGLib.AI.Behaviour.StateMachines.Component
{
    public interface IPriorityStateSo
    {
        public abstract int Priority { get; }
        public abstract PriorityComparerBase_So PriorityComparer { get; }
    }
}
