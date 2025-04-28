using Assets.Scripts.Data.Map;
using Assets.Scripts.Infrastructure.StateMachine.State;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Screens.Map;

namespace Assets.Scripts.GameLogic.Map.StateMachineMap
{
    public class ResultState : IState
    {
        private readonly ScreensSwitcher _screensSwitcher;
        private readonly Map _map;
        private readonly IMapData _mapData;

        public ResultState(ScreensSwitcher screensSwitcher, Map map, IMapData mapData)
        {
            _screensSwitcher = screensSwitcher;
            _map = map;
            _mapData = mapData;
        }

        public void Enter()
        {
            _screensSwitcher.Switch<ResultWindow>();
            _map.StartRotating();
            _mapData.IsChangingStarted = false;
        }

        public void Exit()
        {
            _map.TryStopRotating();
            _map.RotateToStart(callback: _map.Clean);
        }
    }
}
