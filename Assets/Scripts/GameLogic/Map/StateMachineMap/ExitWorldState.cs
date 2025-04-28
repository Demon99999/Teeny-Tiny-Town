using Assets.Scripts.GameLogic.Map.Representation.ActionHandler;
using Assets.Scripts.GameLogic.Map.Representation.Markers;
using Assets.Scripts.GameLogic.Map.Screens;
using Assets.Scripts.Infrastructure.StateMachine;

namespace Assets.Scripts.GameLogic.Map.StateMachineMap
{
    public class ExitWorldState : IPayLoadedState<System.Action>
    {
        private readonly IWorldWindows _worldWindows;
        private readonly MarkersVisibility _markersVisibility;
        private readonly IActionHandlerSwitcher _actionHandlerSwitcher;

        public ExitWorldState(IWorldWindows worldWindows, MarkersVisibility markersVisibility, IActionHandlerSwitcher actionHandlerSwitcher)
        {
            _worldWindows = worldWindows;
            _markersVisibility = markersVisibility;
            _actionHandlerSwitcher = actionHandlerSwitcher;
        }

        public void Enter(System.Action callbakc)
        {
            _markersVisibility.ChangeAllowedVisibility(false);
            _actionHandlerSwitcher.EnterToDefaultState();
            _worldWindows.Remove();
            callbakc?.Invoke();
        }

        public void Exit()
        {

        }
    }
}
