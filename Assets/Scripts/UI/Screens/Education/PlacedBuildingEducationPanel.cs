using Assets.Scripts.GameLogic.Map.Representation;
using Assets.Scripts.GameLogic.Map.Representation.ActionHandler;
using Assets.Scripts.GameLogic.Map.Representation.Markers;
using Assets.Scripts.Services.Input;
using DG.Tweening;
using Zenject;

namespace Assets.Scripts.UI.Screens.Education
{
    public class PlacedBuildingEducationPanel : EducationPanel
    {
        private WorldRepresentationChanger _worldRepresentationChanger;
        private IInputService _inptuService;
        private ActionHandlerStateMachine _actionHandlerStateMachine;
        private MarkersVisibility _markersVisibility;

        [Inject]
        private void Construct(WorldRepresentationChanger worldRepresentationChanger, IInputService inputService, ActionHandlerStateMachine actionHandlerStateMachine, MarkersVisibility markersVisibility)
        {
            _worldRepresentationChanger = worldRepresentationChanger;
            _inptuService = inputService;
            _actionHandlerStateMachine = actionHandlerStateMachine;
            _markersVisibility = markersVisibility;
        }

        public override void Open()
        {
            base.Open();
            _markersVisibility.ChangeAllowedVisibility(true);
            _inptuService.SetEnabled(true);
            _actionHandlerStateMachine.SetActive(true);
            _worldRepresentationChanger.GameplayMoved += OnHandled;
        }

        public override void Hide(TweenCallback callback)
        {
            base.Hide(callback);
            _worldRepresentationChanger.GameplayMoved -= OnHandled;
        }
    }
}
