using System.Collections.Generic;
using UnityEngine;

namespace SGLib.AI.Behaviour.StateMachines.Component
{
    [CreateAssetMenu(menuName = "SG Lib/AI/Behaviour/State Machine/Component/State/Finite State")]
    public class FiniteState_So : StateBase_So
    {
        [SerializeField] private List<FiniteState_So> transitionList;

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

#if UNITY_EDITOR
        private void OnValidate()
        {
            var stateHashSet = new HashSet<FiniteState_So>();

            for (int i = 0; i < transitionList.Count; i++)
            {
                var state = transitionList[i];
                if (state == null) continue;

                if (!stateHashSet.Add(state))
                {
                    transitionList[i] = null;
                }
            }
        }
#endif
    }
}

