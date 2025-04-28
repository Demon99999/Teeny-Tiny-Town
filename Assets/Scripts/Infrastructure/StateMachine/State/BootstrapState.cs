using Assets.Scripts.Infrastructure.State;
using Assets.Scripts.Services.StaticDataServices;

namespace Assets.Scripts.Infrastructure.StateMachine.State
{
    public class BootstrapState : IState
    {
        private readonly IStaticDataService _staticDataService;
        private GameStateMachine _stateMachine;

        public BootstrapState(GameStateMachine stateMachine, IStaticDataService staticDataService)
        {
            _stateMachine = stateMachine;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            RegisterServices();
            _stateMachine.Enter<LoadProgressState>();
        }

        public void Exit()
        {

        }

        private void RegisterServices()
        {
            _staticDataService.Initialize();
        }
    }
}