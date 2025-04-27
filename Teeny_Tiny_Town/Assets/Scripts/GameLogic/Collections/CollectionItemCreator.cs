using System;
using Assets.Scripts.Data;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic.Collections
{
    public class CollectionItemCreator : MonoBehaviour
    {
        [SerializeField] private float _distanceBetweenItems;

        private IPersistantProgrss _persistentProgressService;
        private IMapFactory _mapFactory;
        private IStaticDataService _staticDataService;
        private AnimationsConfig _animationsConfig;

        private TileRepresentation _currentTile;
        private Vector3 _nextItemPosition;
        private Vector3 _previousItemPosition;
        private bool _canChangeItems;

        public event Action<BuildingData> ItemChanged;

        public int CollectionItemIndex { get; private set; }

        [Inject]
        private void Construct(
            IPersistantProgrss persistentProgressService,
            IMapFactory mapFactory,
            IStaticDataService staticDataService)
        {
            _persistentProgressService = persistentProgressService;
            _mapFactory = mapFactory;
            _staticDataService = staticDataService;
            _animationsConfig = _staticDataService.AnimationsConfig;

            CollectionItemIndex = 0;
            _nextItemPosition = new Vector3(transform.position.x + _distanceBetweenItems, transform.position.y, transform.position.z + _distanceBetweenItems);
            _previousItemPosition = new Vector3(transform.position.x - _distanceBetweenItems, transform.position.y, transform.position.z - _distanceBetweenItems);
            _canChangeItems = true;
        }

        private void Start()
        {
            _currentTile = CreateItem(transform.position);
        }

        public void ShowNextBuilding()
        {
            if (_canChangeItems == false)
            {
                return;
            }

            _canChangeItems = false;
            CollectionItemIndex++;

            if (CollectionItemIndex >= _persistentProgressService.Progress.BuildingDatas.Length)
            {
                CollectionItemIndex = 0;
            }

            ChangeTiles(_previousItemPosition, _nextItemPosition, callback: () => _canChangeItems = true);
        }

        public void ShowPreviousBuilding()
        {
            if (_canChangeItems == false)
            {
                return;
            }

            _canChangeItems = false;
            CollectionItemIndex--;

            if (CollectionItemIndex < 0)
            {
                CollectionItemIndex = _persistentProgressService.Progress.BuildingDatas.Length - 1;
            }

            ChangeTiles(_nextItemPosition, _previousItemPosition, callback: () => _canChangeItems = true);
        }

        private void ChangeTiles(Vector3 destroyedTileTargetPosition, Vector3 currentTileStartPosition, TweenCallback callback)
        {
            ItemChanged?.Invoke(_persistentProgressService.Progress.BuildingDatas[CollectionItemIndex]);

            TileRepresentation destroyedTile = _currentTile;
            _currentTile = CreateItem(currentTileStartPosition);

            destroyedTile.transform.DOMove(destroyedTileTargetPosition, _animationsConfig.CollectionItemMoveDuration).onComplete += destroyedTile.Destroy;
            _currentTile.transform.DOMove(transform.position, _animationsConfig.CollectionItemMoveDuration).onComplete = callback;
        }

        private TileRepresentation CreateItem(Vector3 position)
        {
            BuildingData data = _persistentProgressService.Progress.BuildingDatas[CollectionItemIndex];
            BuildingType buildingType = data.Type;

            TileRepresentation tile = _mapFactory.CreateTileRepresentation(position, transform);

            if (buildingType == BuildingType.Lighthouse)
                tile.GroundCreator.Create(TileType.WaterSurface);
            else
                tile.GroundCreator.Create(_staticDataService.GetGroundType(buildingType), RoadType.NonEmpty, GroundRotation.Degrees0, false);

            if (data.IsUnlocked)
                tile.TryChangeBuilding<BuildingRepresentation>(buildingType, false, false);

            return tile;
        }
    }
}
