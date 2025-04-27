using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map.Panels
{
    public class NextBuildingPanel : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        private NextBuildingForPlacingCreator _nextBuildingForPlacingCreator;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(NextBuildingForPlacingCreator nextBuildingForPlacingCreator, IStaticDataService staticDataService)
        {
            _nextBuildingForPlacingCreator = nextBuildingForPlacingCreator;
            _staticDataService = staticDataService;

            OnBuildingForPlacingDataChanged(_nextBuildingForPlacingCreator.BuildingsForPlacingData);

            _nextBuildingForPlacingCreator.DataChanged += OnBuildingForPlacingDataChanged;
        }

        private void OnDestroy()
        {
            _nextBuildingForPlacingCreator.DataChanged -= OnBuildingForPlacingDataChanged;
        }

        private void OnBuildingForPlacingDataChanged(BuildingsForPlacingData data)
        {
            BuildingConfig buildingConfig = _staticDataService.GetBuilding<BuildingConfig>(data.NextBuildingType);
            _icon.sprite = buildingConfig.IconAssetReference;
            _icon.SetNativeSize();
        }
    }
}
