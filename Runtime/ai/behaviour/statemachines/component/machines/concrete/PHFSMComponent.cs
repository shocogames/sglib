using SGLib.AI.Behaviour.StateMachines.Component;
using SGLib.AI.Behaviour.StateMachines.Component.PriorityComparers;
using UnityEngine;

public class PHFSMComponent : HFSM_SGBase<PriorityHierarchicalState_So>
{
#if UNITY_EDITOR
    [SerializeField] private PriorityComparerBase_So priorityComparer;

    private void OnValidate()
    {
        if (OwnedStates == null) return;

        foreach (var node in OwnedStates)
        {
            if (node == null) continue;

            node.PriorityComparer = priorityComparer;
        }
    }
#endif
}