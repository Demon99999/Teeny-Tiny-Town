using Assets.Scripts.Infrastructure.StateMachine.State;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Screens.Map;

namespace Assets.Scripts.GameLogic.Map.StateMachineMap
{
    public class StoreState : IState
    {
        private readonly ScreensSwitcher _windowsSwitcher;

        public StoreState(ScreensSwitcher windowsSwitcher)
        {
            _windowsSwitcher = windowsSwitcher;
        }

        public void Enter()
        {
            _windowsSwitcher.Switch<StoreWindow>();
        }

        public void Exit()
        {

        }
    }
}
