using Assets.Scripts.Infrastructure.StateMachine.State;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Screens.Map;

namespace Assets.Scripts.GameLogic.Map.StateMachineMap
{
    public class WaitingState : IState
    {
        private readonly ScreensSwitcher _screensSwitcher;

        public WaitingState(ScreensSwitcher screensSwitcher)
        {
            _screensSwitcher = screensSwitcher;
        }

        public void Enter()
        {
            _screensSwitcher.Switch<WaitingWindow>();
        }

        public void Exit() { }
    }
}