using Assets.Scripts.Camera;
using Assets.Scripts.Infrastructure.StateMachine.State;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Screens.SelectionMap;
using UnityEngine;

namespace Assets.Scripts.GameLogic.StateMashine
{
    public class MapSelectionState : IState
    {
        private const float CamerStartX = 60.9f;
        private const float CameraStartY = 93.1f;
        private const float CameraStartZ = -60.9f;

        private readonly ScreensSwitcher _screensSwitcher;
        private readonly GameplayCamera _camera;

        private readonly Vector3 _cameraStartPosition;

        public MapSelectionState(ScreensSwitcher screensSwitcher, GameplayCamera camera)
        {
            _screensSwitcher = screensSwitcher;
            _camera = camera;

            _cameraStartPosition = new Vector3(CamerStartX, CameraStartY, CameraStartZ);
        }

        public void Enter()
        {
            _screensSwitcher.Switch<MapSelectionScreen>();
            _camera.MoveTo(_cameraStartPosition);
        }

        public void Exit() { }
    }
}
