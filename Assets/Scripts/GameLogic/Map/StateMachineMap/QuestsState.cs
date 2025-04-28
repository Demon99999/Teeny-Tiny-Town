using Assets.Scripts.Infrastructure.StateMachine.State;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Screens.Map;

namespace Assets.Scripts.GameLogic.Map.StateMachineMap
{
    public class QuestsState : IState
    {
        private readonly ScreensSwitcher _screensSwitcher;

        public QuestsState(ScreensSwitcher screensSwitcher)
        {
            _screensSwitcher = screensSwitcher;
        }

        public void Enter()
        {
            _screensSwitcher.Switch<WorldQuestsWindow>();
        }

        public void Exit()
        {

        }
    }
}
