using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Representation.ActionHandler;
using Assets.Scripts.GameLogic.Map.Representation.Markers;
using Assets.Scripts.Services.Input;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using DG.Tweening;
using Zenject;

namespace Assets.Scripts.UI.Screens.Education
{
    public class MergedBuildingPanel : EducationPanel
    {
        private IMapData _mapData;
        private IInputService _inptuService;
        private ActionHandlerStateMachine _actionHandlerStateMachine;
        private MarkersVisibility _markersVisibility;

        [Inject]
        private void Construct(IMapData mapData, IInputService inputService, ActionHandlerStateMachine actionHandlerStateMachine, MarkersVisibility markersVisibility)
        {
            _mapData = mapData;
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
            _mapData.BuildingUpgraded += OnBuildingUpgraded;
        }

        public override void Hide(TweenCallback callback)
        {
            base.Hide(callback);
            _mapData.BuildingUpgraded -= OnBuildingUpgraded;
        }

        private void OnBuildingUpgraded(BuildingType obj)
        {
            OnHandled();
        }
    }
}
