using Assets.Scripts.GameLogic.Map.StateMachineMap;
using Assets.Scripts.UI.Screens.Map.Reward;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map
{
    public class RewardWindow : BluredBackgroundWindow
    {
        [SerializeField] private RewardsList _rewardsList;

        private WorldStateMachine _worldStateMachine;

        [Inject]
        private void Construct(WorldStateMachine worldStateMachine)
        {
            _worldStateMachine = worldStateMachine;

            _rewardsList.RewardChoosed += OnRewardChoosed;
        }

        private void OnDisable()
        {
            _rewardsList.RewardChoosed -= OnRewardChoosed;
        }

        private void OnRewardChoosed()
        {
            _worldStateMachine.Enter<WorldStartState>();
        }
    }
}
