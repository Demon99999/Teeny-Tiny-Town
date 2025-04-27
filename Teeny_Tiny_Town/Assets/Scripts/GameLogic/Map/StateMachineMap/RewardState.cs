using Assets.Scripts.GameLogic.Points;
using Assets.Scripts.Infrastructure.StateMachine.State;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Screens.Map;

namespace Assets.Scripts.GameLogic.Map.StateMachineMap
{
    public class RewardState : IState
    {
        private readonly ScreensSwitcher _screensSwitcher;
        private readonly RewardsCreator _rewardsCreator;

        public RewardState(ScreensSwitcher screensSwitcher, RewardsCreator rewardsCreator)
        {
            _screensSwitcher = screensSwitcher;
            _rewardsCreator = rewardsCreator;
        }

        public void Enter()
        {
            _screensSwitcher.Switch<RewardWindow>();
            _rewardsCreator.CreateRewards();
        }

        public void Exit() { }
    }
}
