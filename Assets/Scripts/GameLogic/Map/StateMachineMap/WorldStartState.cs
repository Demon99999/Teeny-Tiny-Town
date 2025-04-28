using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Screens;
using Assets.Scripts.Infrastructure.StateMachine.State;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Screens.Map;

namespace Assets.Scripts.GameLogic.Map.StateMachineMap
{
    public class WorldStartState : IState
    {
        private readonly IMapData _worldData;
        private readonly ScreensSwitcher _windowsSwitcher;
        private readonly WorldStateMachine _worldStateMachine;
        private readonly IPersistantProgrss _persistentProgressService;
        private readonly IWorldWindows _worldWindows;

        public WorldStartState(
            IMapData worldData,
            ScreensSwitcher windowsSwitcher,
            WorldStateMachine worldStateMachine,
            IPersistantProgrss persistentProgressService,
            IWorldWindows worldWindows)
        {
            _worldData = worldData;
            _windowsSwitcher = windowsSwitcher;
            _worldStateMachine = worldStateMachine;
            _persistentProgressService = persistentProgressService;
            _worldWindows = worldWindows;
        }

        public void Enter()
        {
            if (_worldWindows.IsRegistered == false)
            {
                _worldWindows.Register();
            }

            if (_persistentProgressService.Progress.GameplayMovesCounter.CanMove == false)
            {
                _worldStateMachine.Enter<WaitingState>();
            }
            else if (_worldData.IsChangingStarted)
            {
                _worldStateMachine.Enter<WorldChangingState>();
            }
            else
            {
                ShowAdditionalBonusOffer();
            }
        }

        public void Exit()
        {

        }

        private void ShowAdditionalBonusOffer()
        {
            _windowsSwitcher.Switch<AdditionalBonusOfferWindow>();
        }
    }
}