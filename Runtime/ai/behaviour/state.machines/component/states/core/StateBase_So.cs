using System;
using SGLib.AI.Behaviour.StateMachines.Component;
using SGLib.Utility.Debug.Logging;
using SGLib.Utility.Patterns.EventChannels.Concrete;
using UnityEngine;

namespace SGLib.AI.Behaviour.StateMachines.Component
{
    public abstract class StateBase_So : ScriptableObject, IStateSo
    {
#if UNITY_EDITOR
        [SerializeField] private bool ActivateLogs = true;
#endif
        [SerializeField] protected bool IsReenterable;

        [Header("State events")]
        [SerializeField] private VoidChannel_So onEnter;
        [SerializeField] private VoidChannel_So onExit;
        [SerializeField] private VoidChannel_So onEnterCondition;
        [SerializeField] private VoidChannel_So onExitCondition;

        [field: NonSerialized] public bool EnterCondition { get; private set; }
        [field: NonSerialized] public bool ExitCondition { get; private set; }
        [NonSerialized] protected IStateMachineComponent Owner;

        public bool Bind(IStateMachineComponent stateMachine)
        {
            if (Owner != null && Owner != stateMachine)
            {
                SGLogger.LogError($"'{name}' is owned by antoher state machine.", this);
                return false;
            }

            if (Owner == stateMachine) return true;

            Owner = stateMachine;
            ResetRuntimeData();

            if (onEnterCondition != null) onEnterCondition.Subscribe(EnableEnter);
            if (onExitCondition != null) onExitCondition.Subscribe(EnableExit);

            return true;
        }

        public void Unbind(IStateMachineComponent stateMachine)
        {
            if (Owner != stateMachine) return;

            if (onEnterCondition != null) onEnterCondition.Unsubscribe(EnableEnter);
            if (onExitCondition != null) onExitCondition.Unsubscribe(EnableExit);

            Owner = null;
            ResetRuntimeData();
        }

        public virtual void ResetRuntimeData()
        {
            DisableEnter();
            DisableExit();
        }

        public virtual void Enter()
        {
#if UNITY_EDITOR
            LogEnter();
#endif

            ResetRuntimeData();
            if (onEnter != null) onEnter.TriggerEvent();
        }

        public abstract void EvaluateTransitions();

        public virtual void Exit()
        {
#if UNITY_EDITOR
            LogExit();
#endif

            DisableExit();
            if (onExit != null) onExit.TriggerEvent();
        }

        protected void EnableEnter() => EnterCondition = true;

        protected void EnableExit()
        {
            if (Owner == null) return;
            if (!Owner.IsCurrentState(this)) return;

            ExitCondition = true;
        }
        public void DisableEnter() => EnterCondition = false;
        protected void DisableExit() => ExitCondition = false;

#if UNITY_EDITOR
        private void LogEnter()
        {
            if (!ActivateLogs) return;

            SGLogger.Log($"{name}: Enter");
        }

        private void LogExit()
        {
            if (!ActivateLogs) return;

            SGLogger.Log($"{name}: Exit");
        }
#endif
    }
}
