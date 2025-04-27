using Assets.Scripts.GameLogic.Map;
using Assets.Scripts.Infrastructure.StateMachine;
using Assets.Scripts.UI;

namespace Assets.Scripts.GameLogic.StateMashine
{
    public class GameplayLoopState : IPayLoadedState<bool>
    {
        private readonly MapSwitcher _mapSwitcher;
        private readonly ScreensSwitcher _screensSwitcher;

        public GameplayLoopState(MapSwitcher mapSwitcher, ScreensSwitcher screensSwitcher)
        {
            _mapSwitcher = mapSwitcher;
            _screensSwitcher = screensSwitcher;
        }

        public void Enter(bool startCurrentWorld)
        {
            _screensSwitcher.HideCurrentWindow();

            if (startCurrentWorld)
            {
                _mapSwitcher.StartCurrentWorld();
            }
            else
            {
                _mapSwitcher.StartLastPlayedWorld();
            }
        }

        public void Exit() { }
    }
}