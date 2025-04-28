using Assets.Scripts.Camera;
using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.GameLogic.Map.Representation;
using Assets.Scripts.GameLogic.Map.Representation.ActionHandler;
using Assets.Scripts.GameLogic.Map.Representation.Markers;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using Assets.Scripts.GameLogic.Map.Screens;
using Assets.Scripts.GameLogic.Map.StateMachineMap;
using Assets.Scripts.GameLogic.Mover;
using Assets.Scripts.GameLogic.Points;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Infrastructure.StateMachine;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.UI.Screens.Map.Panels;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic.Map.Installer
{
    public class WorldInstaller : MonoInstaller
    {
        [SerializeField] private LayerMask _actionHandlerLayerMask;
        [SerializeField] private Map _map;
        [SerializeField] private WorldGenerator _worldGenerator;
        [SerializeField] private TileRepresentation _tileRepresentation;
        [SerializeField] private SelectFrame _selectFrame;
        [SerializeField] private BuildingMarker _buildingMarker;
        [SerializeField] protected RemainingMovesPanel RemainingMovesPanel;
        [SerializeField] private GameplayCamera _gameplayCamera;

        private MapSwitcher _mapSwitcher;

        protected string MapDataId => _mapSwitcher.CurrentWorldDataId ?? PersistentProgressService.Progress.LastPlayedWorldDataId;
        protected IPersistantProgrss PersistentProgressService { get; private set; }

        [Inject]
        private void Construct(IPersistantProgrss persistentProgressService, MapSwitcher mapSwitcher)
        {
            PersistentProgressService = persistentProgressService;
            _mapSwitcher = mapSwitcher;
        }

        public override void InstallBindings()
        {
            BindWorldBootstrapper();
            BindWorldData();
            BindWorldChanger();
            BindActionHandlerLayerMask();
            BindActionHandlerStateMachine();
            BindWorldRepresentationChanger();
            BindWorldFactory();
            BindGameplayMover();
            BindWorldStateMachine();
            BindUiFactory();
            BindNextBuildingForPlacingCreator();
            BindWorld();
            BindPointsCounter();
            BindRewardsCreator();
            BindQuestsChecker();
            BindWorldWindows();
            BindMarkersVisibility();
            BindRewarder();
            BindAcitonHandlerSwitcher();
            BindWorldCleaner();

            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.BindFactory<GameplayCamera, GameplayCamera.Factory>().FromComponentInNewPrefab(_gameplayCamera.gameObject);
        }

        protected virtual void BindAcitonHandlerSwitcher()
        {
            Container.BindInterfacesTo<ActionHandlerSwitcher>().AsSingle();
        }

        protected virtual void BindWorldWindows()
        {
            Container.BindInterfacesTo<WorldWindows>().AsSingle();
        }

        protected virtual void BindWorldBootstrapper()
        {
            Container.BindInterfacesAndSelfTo<MapBootstrapper>().AsSingle().NonLazy();
        }

        protected virtual void BindWorldData()
        {
            Container.BindInterfacesTo<MapData>().FromInstance(PersistentProgressService.Progress.GetMapData(MapDataId)).AsSingle();
        }

        protected virtual void BindWorldChanger()
        {
            Container.BindInterfacesTo<WorldChanger>().AsSingle();
        }

        protected virtual void BindGameplayMover()
        {
            Container.BindInterfacesTo<GameplayMover>().AsSingle();
        }

        private void BindWorldCleaner()
        {
            Container.BindInterfacesAndSelfTo<WorldCleaner>().AsSingle().NonLazy();
        }

        private void BindRewarder()
        {
            Container.Bind<Rewarder>().AsSingle();
        }

        private void BindMarkersVisibility()
        {
            Container.Bind<MarkersVisibility>().AsSingle();
        }

        private void BindQuestsChecker()
        {
            Container.BindInterfacesAndSelfTo<QuestsChecker>().AsSingle().NonLazy();
        }

        private void BindRewardsCreator()
        {
            Container.Bind<RewardsCreator>().AsSingle();
        }

        private void BindPointsCounter()
        {
            Container.BindInterfacesAndSelfTo<PointsCounter>().AsSingle().NonLazy();
        }

        private void BindWorld()
        {
            Container.BindInterfacesAndSelfTo<Map>().FromInstance(_map).AsSingle();
        }

        private void BindNextBuildingForPlacingCreator()
        {
            Container.Bind<NextBuildingForPlacingCreator>().AsSingle();
        }

        private void BindUiFactory()
        {
            Container
                .Bind<IUIFactory>()
                .To<UIFactory>()
                .AsSingle();

            Container.BindFactory<RemainingMovesPanel, RemainingMovesPanel.Factory>()
                .FromComponentInNewPrefab(RemainingMovesPanel);
        }

        private void BindActionHandlerLayerMask()
        {
            Container.BindInstance(_actionHandlerLayerMask).AsSingle();
        }

        private void BindActionHandlerStateMachine()
        {
            Container.BindInterfacesAndSelfTo<ActionHandlerStateMachine>().AsSingle();
            Container.Bind<ActionHandlerStatesFactory>().AsSingle();
        }

        private void BindWorldStateMachine()
        {
            Container.Bind<WorldStateMachine>().AsSingle();
            Container.Bind<StatesFactory>().AsSingle();
        }

        private void BindWorldFactory()
        {
            Container.Bind<IMapFactory>().To<MapFactory>().AsSingle();

            Container.BindFactory<WorldGenerator, WorldGenerator.Factory>()
                .FromComponentInNewPrefab(_worldGenerator)
                .WithGameObjectName("WorldGenerator");

            Container.BindFactory<TileRepresentation, TileRepresentation.Factory>()
                .FromComponentInNewPrefab(_tileRepresentation)
                .WithGameObjectName("TileRepres")
                .UnderTransformGroup("Tiles");

            Container.BindFactory<SelectFrame, SelectFrame.Factory>()
                .FromComponentInNewPrefab(_selectFrame)
                .WithGameObjectName("SelectFrame");

            Container.BindFactory<BuildingMarker, BuildingMarker.Factory>()
                .FromComponentInNewPrefab(_buildingMarker)
                .WithGameObjectName("Marker");
        }

        private void BindWorldRepresentationChanger()
        {
            Container.BindInterfacesAndSelfTo<WorldRepresentationChanger>().AsSingle();
        }
    }
}
