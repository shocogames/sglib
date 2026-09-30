using System.Collections.Generic;
using UnityEngine;

namespace SGLib.AI.Behaviour.StateMachines.Component
{
    [CreateAssetMenu(menuName = "Scriptable Objects/AI/Behaviour/State Machine/Component/State/Hierarchical State")]
    public class HierarchicalState_So : HierarchicalStateBase_So
    {
        [SerializeField] private List<HierarchicalState_So> transitionList;

        public override void EvaluateTransitions()
        {
            if (Owner == null || !ExitCondition) return;

            foreach (var nextState in transitionList)
            {
                if (nextState == null || !nextState.EnterCondition) continue;

                if (!IsReenterable && Owner.IsCurrentState(nextState)) continue;

                Owner.TransitionTo(nextState, allowSameState: IsReenterable);
                return;
            }
        }
    }
}
