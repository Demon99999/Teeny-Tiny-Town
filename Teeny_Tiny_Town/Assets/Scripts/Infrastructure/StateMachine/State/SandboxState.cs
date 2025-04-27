using Assets.Scripts.Services.Input;
using Assets.Scripts.Services.SaveLoadServices;

namespace Assets.Scripts.Infrastructure.StateMachine.State
{
    public class SandboxState : IState
    {
        private const string SandBox = "SandboxScene";

        private readonly ISceneLoader _sceneLoader;
        private readonly IInputService _inputService;
        private readonly ISaveLoadService _saveLoadService;

        public SandboxState(ISceneLoader sceneLoader, IInputService inputService, ISaveLoadService saveLoadService)
        {
            _sceneLoader = sceneLoader;
            _inputService = inputService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            _sceneLoader.Load(SandBox);
            _inputService.SetEnabled(true);
        }

        public void Exit()
        {
            _inputService.SetEnabled(false);
            _saveLoadService.SaveProgress();
        }
    }
}
