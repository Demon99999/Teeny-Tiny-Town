using Assets.Scripts.GameLogic.Map.Representation.Markers;
using Assets.Scripts.Infrastructure.StateMachine.State;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Screens.Map;

namespace Assets.Scripts.GameLogic.Map.StateMachineMap
{
    public class SafeGameplayState : IState
    {
        private readonly ScreensSwitcher _screensSwitcher;
        private readonly MarkersVisibility _markersVisibility;

        public SafeGameplayState(ScreensSwitcher screensSwitcher, MarkersVisibility markersVisibility)
        {
            _screensSwitcher = screensSwitcher;
            _markersVisibility = markersVisibility;
        }

        public void Enter()
        {
            _markersVisibility.ChangeAllowedVisibility(false);
            _screensSwitcher.Switch<SaveGameplayWindow>();
        }

        public void Exit()
        {

        }
    }
}
