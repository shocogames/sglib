using System;
using System.Collections.Generic;
using SGLib.AI.Behaviour.StateMachines.Component.PriorityComparers;
using SGLib.Utility.Debug.Logging;
using UnityEngine;

namespace SGLib.AI.Behaviour.StateMachines.Component
{
    [CreateAssetMenu(menuName = "Scriptable Objects/AI/Behaviour/State Machine/Component/State/Priority Hierarchical State")]
    public class PriorityHierarchicalState_So : HierarchicalStateBase_So, IPriorityStateSo
    {
        [Header("State configuration")]
        [SerializeField, Min(0)] private int priority;
        [SerializeField] private bool isInterruptible;
        [SerializeField] private PriorityComparerBase_So priorityComparer;
        [SerializeField] private List<PriorityHierarchicalState_So> transitionList;

        public int Priority => priority;
        public PriorityComparerBase_So PriorityComparer
        {
            get => priorityComparer;
            set => priorityComparer = value;
        }

        public override void EvaluateTransitions()
        {
            if (priority < 0)
                throw new InvalidOperationException(
                    "this is a transition node there should not be any transition evaluation. It cannot be the current leaf.");

            // Transition checker
            if (!ExitCondition && !isInterruptible) return;

            for (int i = 0; i < transitionList.Count; i++)
            {
                var nextState = transitionList[i];
                if (nextState == null) continue;

                if (!nextState.EnterCondition) continue;

                if (Owner.IsSameState(nextState, this))
                {
                    if (!IsReenterable) continue;

                    Owner.TransitionTo(nextState);
                    return;
                }

                // can transition to a state with higher priority if it is an interruption
                bool isInterruption = !ExitCondition && isInterruptible;
                var higherPriorityState = GetHigherPriorityState(this, nextState);
                bool isHigherPriorityState = !Owner.IsSameState(higherPriorityState, this);

                if (isInterruption && !isHigherPriorityState) break;

                Owner.TransitionTo(nextState);
                break;
            }
        }

        private PriorityHierarchicalState_So GetHigherPriorityState(PriorityHierarchicalState_So t1, PriorityHierarchicalState_So t2)
        {
            return priorityComparer.Compare(t1, t2) <= 0 ? t1 : t2;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            // no transition nodes required.
            if (priority < 0) return;

            // it is the root node
            if (Ancestor == null) return;

            if (transitionList == null || transitionList.Count == 0)
            {
                SGLogger.LogWarning($"(empty) no transitions set on state {name}.");
                return;
            }

            if (priorityComparer == null)
            {
                SGLogger.LogWarning($"(empty) no priority comparer set on state {name}.");
                return;
            }

            transitionList.Sort(priorityComparer);
        }
#endif
    }
}
