namespace SGLib.AI.Behaviour.StateMachines.Classic
{
    public interface IStateMachine
    {
        public void Execute(float deltaTime);
        public void TransitionTo(IState newState, bool doEnter = true, bool doExit = true, bool allowSameState = false);
        public void RevertToPrevious(bool doEnter = true, bool doExit = true, bool allowSameState = false);
        public bool IsSameState(IState s1, IState s2);
        public void Run();
        public void Stop();
    }
}