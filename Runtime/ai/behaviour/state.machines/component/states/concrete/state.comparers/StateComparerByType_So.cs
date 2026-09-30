using UnityEngine;

namespace SGLib.AI.Behaviour.StateMachines.Component
{
    [CreateAssetMenu(menuName = "SG Lib/AI/Behaviour/State Machine/Classic/State/State Comparer/By Type")]
    public class StateComparerByType_So : StateComparerBase_So
    {
        public override bool Equals(IStateSo x, IStateSo y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null || y == null) return false;
            return x.GetType() == y.GetType();
        }

        public override int GetHashCode(IStateSo state) =>
            state?.GetType().GetHashCode() ?? 0;
    }
}
