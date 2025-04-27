namespace Assets.Scripts.Infrastructure.StateMachine
{
    public interface IPayLoadedState<TPayLoad> : IExitableState
    {
        void Enter(TPayLoad payLoad);
    }
}