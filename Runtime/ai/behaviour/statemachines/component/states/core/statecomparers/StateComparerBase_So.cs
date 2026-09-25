using System.Collections.Generic;
using UnityEngine;

namespace SGLib.AI.Behaviour.StateMachines.Component
{
    public abstract class StateComparerBase_So : ScriptableObject, IEqualityComparer<IStateSo>
    {
        public abstract bool Equals(IStateSo x, IStateSo y);
        public abstract int GetHashCode(IStateSo state);
    }
}

