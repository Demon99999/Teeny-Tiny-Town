using Assets.Scripts.Camera;
using Assets.Scripts.GameLogic;
using Assets.Scripts.GameLogic.Map;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private const float CamerStartX = 67.3f;
        private const float CameraStartY = 93.1f;
        private const float CameraStartZ = -67.3f;

        private readonly DiContainer _container;
        private readonly MapSwitcher.Factory _mapFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly GamePlane.Factory _gamePlaneFactory;
        private readonly GameplayCamera.Factory _cameraFactory;

        private readonly Vector3 _cameraStartPosition;

        public GameFactory(
            DiContainer container,
            MapSwitcher.Factory mapFactory,
            IStaticDataService staticDataService,
            GamePlane.Factory gamePlane,
            GameplayCamera.Factory cameraFactory)
        {
            _container = container;
            _staticDataService = staticDataService;
            _mapFactory = mapFactory;
            _gamePlaneFactory = gamePlane;
            _cameraFactory = cameraFactory;

            _cameraStartPosition = new Vector3(CamerStartX, CameraStartY, CameraStartZ);
        }

        public void CreatePlane() => _gamePlaneFactory.Create();

        public GameplayCamera CreateCamera()
        {
            GameplayCamera camera = _cameraFactory.Create();
            camera.gameObject.transform.position = _cameraStartPosition;
            BindInstance(camera);
            return camera;
        }

        public MapSwitcher CreateMapSwitcher()
        {
            MapSwitcher mapSwitcher = _mapFactory.Create();
            BindInstance(mapSwitcher);
            return mapSwitcher;
        }

        public void CreateUiSoundPlayer()
        {
            CreateAndBindSoundPlayer(_staticDataService.MapsConfig.UiSoundPlayer);
        }

        public void CreateWorldWalletSoundPlayer()
        {
            CreateAndBindSoundPlayer(_staticDataService.MapsConfig.WorldWalletSoundPlayer);
        }

        public Map CreateEducationWorld(Vector3 position, Transform parent)
        {
            return CreateWorldInternal(_staticDataService.MapsConfig.EducationMap, position, parent);
        }

        public Map CreateMap(string id, Vector3 position, Transform parent)
        {
            return CreateWorldInternal(_staticDataService.GetMap<MapConfig>(id).MapTemplate, position, parent);
        }

        private Map CreateWorldInternal(Map mapPrefab, Vector3 position, Transform parent)
        {
            var mapGameObject = _container.InstantiatePrefab(mapPrefab, parent);
            mapGameObject.transform.SetLocalPositionAndRotation(position, Quaternion.identity);
            return mapGameObject.GetComponent<Map>();
        }

        private void CreateAndBindSoundPlayer<T>(T prefabComponent) where T : Component
        {
            var playerObject = _container.InstantiatePrefab(prefabComponent.gameObject);
            var component = playerObject.GetComponent<T>();

            BindInstance(component);
        }

        private void BindInstance<T>(T instance) => _container.BindInstance(instance).AsSingle();
    }
}