using System;
using Assets.Scripts.GameLogic.Collections;
using Assets.Scripts.GameLogic.Map;
using Assets.Scripts.GameLogic.Map.Representation.Markers;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using Assets.Scripts.GameLogic.SandBox;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure.Factories
{
    public class MapFactory : IMapFactory
    {
        private readonly DiContainer _container;
        private readonly WorldGenerator.Factory _worldGeneratorFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly SelectFrame.Factory _selectFrameFactory;
        private readonly TileRepresentation.Factory _tileRepresentationFactory;
        private readonly BuildingMarker.Factory _buildingMarkerFactory;
        private readonly IMapRotation _mapRotation;

        public WorldGenerator WorldGenerator { get; private set; }

        public MapFactory(
            DiContainer container,
            WorldGenerator.Factory worldGeneratorFactory,
            SelectFrame.Factory selectFrameFactory,
            TileRepresentation.Factory tileRepresentationFactory,
            BuildingMarker.Factory buildingMarkerFactory,
            IStaticDataService staticDataService,
            IMapRotation mapRotation)
        {
            _container = container;
            _worldGeneratorFactory = worldGeneratorFactory;
            _selectFrameFactory = selectFrameFactory;
            _tileRepresentationFactory = tileRepresentationFactory;
            _buildingMarkerFactory = buildingMarkerFactory;
            _staticDataService = staticDataService;
            _mapRotation = mapRotation;
        }

        public BuildingRepresentation CreateBuilding(BuildingType buildingType, Vector3 position, Transform parent)
        {
            BuildingConfig config = _staticDataService.GetBuilding<BuildingConfig>(buildingType);
            var building = InstantiatePrefabComponent(config.Prefab, position, parent);

            _container.Inject(building);
            building.Init(buildingType);
            return building;
        }

        public void CreateBuildingMarker(Transform parent)
        {
            CreateAndSetup(_buildingMarkerFactory.Create, Vector3.zero, parent, true);
        }

        public void CreateCollectionItemCreator()
        {
            var creatorPrefab = _container.InstantiatePrefab(_staticDataService.MapsConfig.CollectionItemCreator);
            CollectionItemCreator creator = creatorPrefab.GetComponent<CollectionItemCreator>();
            BindToContainer(creator);
        }

        public Ground CreateGround(TileType tileType, Vector3 position, Transform parent)
        {
            var config = _staticDataService.GetDefaultGround(tileType);
            return InstantiatePrefabComponent(config.Prefab, position, parent);
        }

        public Ground CreateGround(GroundType groundType, RoadType roadType, Vector3 position, GroundRotation rotation, Transform parent)
        {
            var config = _staticDataService.GetRoad(groundType, roadType);
            return InstantiatePrefabComponent(config.Prefab, position, parent, (float)rotation);
        }

        public SandboxWorld CreateSandboxWorld()
        {
            var sandboxPrefab = _container.InstantiatePrefab(_staticDataService.SandboxConfig.SandboxWorld);
            return sandboxPrefab.GetComponent<SandboxWorld>();
        }

        public SelectFrame CreateSelectFrame(Transform parent)
        {
            return CreateAndSetup(_selectFrameFactory.Create, Vector3.zero, parent, true);
        }

        public TileRepresentation CreateTileRepresentation(Vector3 position, Transform parent)
        {
            return CreateAndSetup(_tileRepresentationFactory.Create, position, parent);
        }

        public WorldGenerator CreateWorldGenerator(Transform parent = null)
        {
            WorldGenerator = _worldGeneratorFactory.Create();
            BindToContainer(WorldGenerator);
            BindToContainer(WorldGenerator.GetComponent<BuildingCreator>());
            return WorldGenerator;
        }

        private T InstantiatePrefabComponent<T>(T prefabComponent, Vector3 position, Transform parent, float additionalRotation = 0) where T : Component
        {
            float totalRotation = (additionalRotation + _mapRotation.RotationDegrees) % 360;
            Quaternion rotation = Quaternion.Euler(0, totalRotation, 0);

            var instance = _container.InstantiatePrefab(prefabComponent.gameObject, position, rotation, parent);
            var component = instance.GetComponent<T>();

            return component;
        }

        private void BindToContainer<T>(T instance) where T : MonoBehaviour
        {
            _container.Bind<T>().FromInstance(instance).AsSingle();
        }

        private void SetTransform(Transform target, Vector3 position, Transform parent)
        {
            target.position = position;
            target.SetParent(parent);
        }

        private T CreateAndSetup<T>(Func<T> factory, Vector3 position, Transform parent, bool bindToContainer = false) where T : MonoBehaviour
        {
            T instance = factory();
            SetTransform(instance.transform, position, parent);
            if (bindToContainer) BindToContainer(instance);
            return instance;
        }
    }
}