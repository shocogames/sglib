using UnityEngine;

namespace SGLib.AI.Behaviour.StateMachines.Component
{
    [CreateAssetMenu(menuName = "Scriptable Objects/State Machine/State")]
    public class State_So : StandardStateBase_So<State_So>
    {
        public override void EvaluateTransitions()
        {
            if (Owner == null || !ExitCondition) return;

            foreach (var nextState in TransitionList)
            {
                if (nextState == null || !nextState.EnterCondition) continue;

                if (!IsReenterable && Owner.IsCurrentState(nextState)) continue;

                Owner.TransitionTo(nextState, allowSameState: IsReenterable);
                return;
            }
        }
    }
}

