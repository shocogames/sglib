using System;
using SGLib.Utility.Patterns.EventChannels.Primitive;
using UnityEngine;

namespace SGLib.AI.Behaviour.StateMachines.Classic
{
    [CreateAssetMenu(menuName = "SG Lib/AI/Behaviour/State Machine/Classic/State/Priority Hierarchical State Config")]
    public class PriorityHierarchicalStateConfig_So : StateConfig_So
    {
        [field: SerializeField] public int Priority { get; private set; }
        [field: SerializeField] public bool IsInterruptible { get; private set; }

        [field: SerializeField] public VoidChannel_So OnEnterCondition { get; private set; }
        [field: SerializeField] public VoidChannel_So OnExitCondition { get; private set; }
    }
}
