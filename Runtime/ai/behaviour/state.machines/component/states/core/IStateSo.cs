namespace SGLib.AI.Behaviour.StateMachines.Component
{
    public interface IStateSo
    {
        public void Enter();
        public void EvaluateTransitions();
        public void Exit();

        public bool Bind(IStateMachineComponent stateMachine);
        public void Unbind(IStateMachineComponent stateMachine);
        public void ResetRuntimeData();
    }
}

