using System.Collections.Generic;
using UnityEngine;

namespace SGLib.AI.Behaviour.StateMachines.Component
{
    [CreateAssetMenu(menuName = "Scriptable Objects/State Machine/State")]
    public class State_So : StateBase_So
    {
        [SerializeField] private List<State_So> transitionList;

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
            var stateHashSet = new HashSet<State_So>();

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

