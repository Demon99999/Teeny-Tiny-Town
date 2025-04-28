using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.GameLogic.Map.Representation.ActionHandler;
using Assets.Scripts.GameLogic.Map.Representation.Markers;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.GameLogic.Inventory
{
    public class InventoryCell : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _icon;
        [SerializeField] private CanvasGroup _iconCanvasGroup;
        [SerializeField] private int _serialNumber;

        private ActionHandlerStateMachine _actionHandlerStateMachine;
        private BuildingMarker _buildingMarker;
        private NextBuildingForPlacingCreator _nextBuildingForPlacingCreator;
        private IStaticDataService _staticDataService;
        private IMapData _worldData;

        private BuildingType BuildingType => _worldData.Inventory[_serialNumber];

        [Inject]
        private void Construct(
            ActionHandlerStateMachine actionHandlerStateMachine,
            BuildingMarker buildingMarker,
            NextBuildingForPlacingCreator nextBuildingForPlacingCreator,
            IStaticDataService staticDataService,
            IMapData worldData)
        {
            _actionHandlerStateMachine = actionHandlerStateMachine;
            _buildingMarker = buildingMarker;
            _nextBuildingForPlacingCreator = nextBuildingForPlacingCreator;
            _staticDataService = staticDataService;
            _worldData = worldData;

            ChangeIcon();

            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            if (!(_actionHandlerStateMachine.CurrentState is NewBuildingPlacePositionHandler) || _buildingMarker.IsCreatedBuilding)
            {
                return;
            }

            if (BuildingType != BuildingType.Undefined)
            {
                BuildingType buildingType = _buildingMarker.BuildingType;
                _nextBuildingForPlacingCreator.ChangeCurrentBuildingForPlacing(BuildingType);
                _worldData.Inventory[_serialNumber] = buildingType;
            }
            else
            {
                _worldData.Inventory[_serialNumber] = _buildingMarker.BuildingType;
                _nextBuildingForPlacingCreator.MoveToNextBuilding();
            }

            ChangeIcon();
        }

        private void ChangeIcon()
        {
            if (BuildingType == BuildingType.Undefined)
            {
                _iconCanvasGroup.alpha = 0;

                return;
            }

            BuildingConfig buildingConfig = _staticDataService.GetBuilding<BuildingConfig>(BuildingType);
            _icon.sprite = buildingConfig.IconAssetReference;
            _icon.SetNativeSize();
            _iconCanvasGroup.alpha = 1;
        }
    }
}
