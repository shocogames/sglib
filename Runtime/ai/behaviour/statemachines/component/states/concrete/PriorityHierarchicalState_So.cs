using SGLib.AI.Behaviour.StateMachines.Component.PriorityComparers;
using SGLib.Utility.Debug.Logging;
using UnityEngine;

namespace SGLib.AI.Behaviour.StateMachines.Component
{
    [CreateAssetMenu(menuName = "Scriptable Objects/State Machine/Priority Hierarchical State")]
    public class PriorityHierarchicalState_So : HierarchicalStateBase_So<PriorityHierarchicalState_So>, IPriorityStateSo
    {
        [Header("State configuration")]
        [SerializeField, Min(0)] private int priority;
        [SerializeField] private bool isInterruptible;
        [SerializeField] private PriorityComparerBase_So priorityComparer;

        public int Priority => priority;
        public PriorityComparerBase_So PriorityComparer
        {
            get => priorityComparer;
            set => priorityComparer = value;
        }

        public override void EvaluateTransitions()
        {
            // Transition checker
            if (!ExitCondition && !isInterruptible) return;

            for (int i = 0; i < TransitionList.Count; i++)
            {
                var nextState = TransitionList[i];
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
            if (Ancestor == null) return;

            if (TransitionList == null || TransitionList.Count == 0)
            {
                SGLogger.LogWarning($"(empty) no transitions set on state {name}.");
                return;
            }

            if (priorityComparer == null)
            {
                SGLogger.LogWarning($"(empty) no priority comparer set on state {name}.");
                return;
            }

            TransitionList.Sort(priorityComparer);
        }
#endif
    }
}
