using System.Collections.Generic;
using UnityEngine;

namespace SGLib.AI.Behaviour.StateMachines.Component.PriorityComparers
{
    [CreateAssetMenu(menuName = "SG Lib/AI/Behaviour/State Machine/Component/State/Priority Comparer/Ascendant Priority")]
    public sealed class AscendantPriority_So : PriorityComparerBase_So
    {
        public override int Compare(IPriorityStateSo x, IPriorityStateSo y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return 1;
            if (y == null) return -1;

            int xp = x.Priority;
            int yp = y.Priority;

            return xp.CompareTo(yp);
        }
    }
}
