using SGLib.AI.Behaviour.StateMachines;

namespace SGLib.AI.Behaviour.StateMachines.Classic
{
    public interface IState
    {
        public void Enter();
        public void Execute(IStateMachine stateMachine, float deltaTime);
        public void Exit();
    }
}
