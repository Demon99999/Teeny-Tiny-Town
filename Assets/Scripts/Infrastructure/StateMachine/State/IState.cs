namespace Assets.Scripts.Infrastructure.StateMachine.State
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}