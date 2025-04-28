using Assets.Scripts.Infrastructure.StateMachine.State;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Screens;

namespace Assets.Scripts.GameLogic.StateMashine
{
    public class ShowQuestsState : IState
    {
        private readonly ScreensSwitcher _screensSwitcher;

        public ShowQuestsState(ScreensSwitcher screensSwitcher)
        {
            _screensSwitcher = screensSwitcher;
        }

        public void Enter()
        {
            _screensSwitcher.Switch<GameplayQuestsWindow>();
        }

        public void Exit()
        {

        }
    }
}
