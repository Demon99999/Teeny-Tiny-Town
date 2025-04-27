using Assets.Scripts.Camera;
using Assets.Scripts.Infrastructure.StateMachine.State;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Screens.ScreenStart;
using UnityEngine;

namespace Assets.Scripts.GameLogic.StateMashine
{
    public class GameStartState : IState
    {
        private const float CamerStartX = 67.3f;
        private const float CameraStartY = 93.1f;
        private const float CameraStartZ = -67.3f;

        private readonly ScreensSwitcher _screensSwitcher;
        private readonly GameplayCamera _camera;
        private readonly IPersistantProgrss _persistentProgressService;
        private readonly GameplayStateMachine _gameplayStateMachine;

        private readonly Vector3 _cameraStartPosition;

        public GameStartState(ScreensSwitcher screensSwitcher, GameplayCamera camera, IPersistantProgrss persistentProgressService, GameplayStateMachine gameplayStateMachine)
        {
            _screensSwitcher = screensSwitcher;
            _camera = camera;
            _persistentProgressService = persistentProgressService;
            _gameplayStateMachine = gameplayStateMachine;

            _cameraStartPosition = new Vector3(CamerStartX, CameraStartY, CameraStartZ);
        }

        public void Enter()
        {
            _screensSwitcher.Switch<StartScreen>();
            _camera.MoveTo(_cameraStartPosition);

            if (_persistentProgressService.Progress.IsEducationCompleted)
            {
                _screensSwitcher.Switch<StartScreen>();
                _camera.MoveTo(_cameraStartPosition);
            }
            else
            {
                _gameplayStateMachine.Enter<GameplayLoopState, bool>(true);
            }
        }

        public void Exit() { }
    }
}
