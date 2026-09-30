namespace SGLib.AI.Behaviour.StateMachines.Component
{
    public interface IStateMachineComponent
    {
        public void TransitionTo(IStateSo newState, bool doEnter = true, bool doExit = true, bool allowSameState = false);
        public void RevertToPrevious(bool doEnter = true, bool doExit = true, bool allowSameState = false);
        public bool IsSameState(IStateSo s1, IStateSo s2);
        public bool IsCurrentState(IStateSo state);
        public void Run();
        public void Stop();
        public void Resume();
        public void Pause();
    }
}
