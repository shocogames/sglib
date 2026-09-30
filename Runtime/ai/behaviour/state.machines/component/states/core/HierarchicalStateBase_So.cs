using System.Collections.Generic;
using UnityEngine;

namespace SGLib.AI.Behaviour.StateMachines.Component
{
    public abstract class HierarchicalStateBase_So : StateBase_So
    {
        [field: SerializeField] public HierarchicalStateBase_So Ancestor { get; private set; }
    }
}
