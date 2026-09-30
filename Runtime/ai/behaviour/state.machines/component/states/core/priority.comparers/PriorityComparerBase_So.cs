using System.Collections.Generic;
using UnityEngine;

namespace SGLib.AI.Behaviour.StateMachines.Component.PriorityComparers
{
    public abstract class PriorityComparerBase_So : ScriptableObject, IComparer<IPriorityStateSo>
    {
        public abstract int Compare(IPriorityStateSo x, IPriorityStateSo y);
    }
}