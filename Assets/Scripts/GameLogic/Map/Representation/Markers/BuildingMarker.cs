using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic.Map.Representation.Markers
{
    public class BuildingMarker : MonoBehaviour
    {
        private IMapFactory _mapFactory;
        private NextBuildingForPlacingCreator _nextBuildingForPlacingCreator;

        private BuildingRepresentation _building;
        private bool _isHided;

        public TileRepresentation MarkedTile { get; private set; }
        public bool IsCreatedBuilding { get; private set; }
        public BuildingType BuildingType => _building.Type;

        [Inject]
        private void Construct(IMapFactory mapFactory, NextBuildingForPlacingCreator nextBuildingForPlacingCreator)
        {
            _mapFactory = mapFactory;
            _nextBuildingForPlacingCreator = nextBuildingForPlacingCreator;

            IsCreatedBuilding = false;
            _isHided = true;

            _nextBuildingForPlacingCreator.DataChanged += OnNextBuildingForPlacingDataChanged;
        }

        private void OnDestroy()
        {
            _nextBuildingForPlacingCreator.DataChanged -= OnNextBuildingForPlacingDataChanged;
        }

        public void Mark(TileRepresentation tile)
        {
            if (tile == null)
            {
                return;
            }

            transform.position = tile.BuildingPoint.position;
            MarkedTile = tile;
        }

        public void Show()
        {
            if (_building != null)
            {
                _building.gameObject.SetActive(true);
                _building.Blink();
            }

            _isHided = false;
        }

        public void Hide()
        {
            if (_building != null)
            {
                _building.gameObject.SetActive(false);
                _building.StopBlinking();
            }

            _isHided = true;
        }

        public void Replace(Vector3 targetPosition)
        {
            transform.position = targetPosition;
        }

        private void TryUpdate(BuildingType targetBuildingType)
        {
            if (targetBuildingType == BuildingType.Undefined)
                Debug.LogError("Building type can not be undefined");

            if (IsCreatedBuilding)
                Debug.LogError("The building is not yet complete");

            if (_building == null || _building.Type != targetBuildingType)
            {
                _building?.Destroy();

                IsCreatedBuilding = true;

                _building = _mapFactory.CreateBuilding(targetBuildingType, transform.position, transform);

                if (_building.transform.position != transform.position)
                {
                    _building.transform.position = transform.position;
                }

                if (_isHided)
                {
                    _building.gameObject.SetActive(false);
                }

                IsCreatedBuilding = false;
            }
        }

        private void OnNextBuildingForPlacingDataChanged(BuildingsForPlacingData data)
        {
            TryUpdate(data.CurrentBuildingType);
        }

        public class Factory : PlaceholderFactory<BuildingMarker>
        {

        }
    }
}
