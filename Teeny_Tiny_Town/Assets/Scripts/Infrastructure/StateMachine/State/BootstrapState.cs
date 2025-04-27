using Assets.Scripts.Infrastructure.State;
using Assets.Scripts.Services.StaticDataServices;

namespace Assets.Scripts.Infrastructure.StateMachine.State
{
    public class BootstrapState : IState
    {
        private GameStateMachine _stateMachine;
        private readonly IStaticDataService _staticDataService;

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

        public void Exit() { }
        
        private void RegisterServices()
        {
            _staticDataService.Initialize();
        }
    }
}