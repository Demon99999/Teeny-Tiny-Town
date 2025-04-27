using Assets.Scripts.Audio;
using Assets.Scripts.Camera;
using Assets.Scripts.GameLogic.Map;
using Assets.Scripts.GameLogic.Map.Representation.Markers;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Screens.Map.Panels;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic.Collections
{
    public class CollectionInstaller : MonoInstaller
    {
        [SerializeField] private LayerMask _layerMask;

        [SerializeField] private WorldGenerator _worldGenerator;
        [SerializeField] private TileRepresentation _tileRepresentation;
        [SerializeField] private SelectFrame _selectFrame;
        [SerializeField] private BuildingMarker _buildingMarker;

        [SerializeField] private MapSwitcher _mapSwitcher;
        [SerializeField] private GamePlane _gamePlane;
        [SerializeField] private GameplayCamera _gameplayCamera;
        [SerializeField] private Blur _blur;
        [SerializeField] private UiSoundPlayer _uiSoundPlayer;
        [SerializeField] private WorldWalletSoundPlayer _worldWalletSoundPlayer;
        [SerializeField] protected RemainingMovesPanel RemainingMovesPanel;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CollectionBootstrapper>().AsSingle().NonLazy();
            BindUiFactory();
            BindMapFactory();
            Container.BindInterfacesTo<CollectionRotation>().AsSingle();
            BindGameFactory();
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

        private void BindGameFactory()
        {
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();

            Container.BindFactory<MapSwitcher, MapSwitcher.Factory>()
                .FromComponentInNewPrefab(_mapSwitcher.gameObject);

            Container.BindInterfacesAndSelfTo<ScreensSwitcher>().AsSingle();

            Container.BindFactory<GamePlane, GamePlane.Factory>()
                .FromComponentInNewPrefab(_gamePlane.gameObject);

            Container.BindFactory<GameplayCamera, GameplayCamera.Factory>()
                .FromComponentInNewPrefab(_gameplayCamera.gameObject);

            Container.BindFactory<UiSoundPlayer, UiSoundPlayer.Factory>()
                .FromComponentInNewPrefab(_uiSoundPlayer);

            Container.BindFactory<WorldWalletSoundPlayer, WorldWalletSoundPlayer.Factory>()
                .FromComponentInNewPrefab(_worldWalletSoundPlayer);

            Container.BindFactory<Blur, Blur.BlurFactory>()
                .FromComponentInNewPrefab(_blur);
        }

        private void BindMapFactory()
        {
            Container.Bind<IMapFactory>().To<MapFactory>().AsSingle();

            Container.BindFactory<WorldGenerator, WorldGenerator.Factory>()
                .FromComponentInNewPrefab(_worldGenerator);

            Container.BindFactory<TileRepresentation, TileRepresentation.Factory>()
                .FromComponentInNewPrefab(_tileRepresentation);

            Container.BindFactory<SelectFrame, SelectFrame.Factory>()
                .FromComponentInNewPrefab(_selectFrame);

            Container.BindFactory<BuildingMarker, BuildingMarker.Factory>()
                .FromComponentInNewPrefab(_buildingMarker);
        }
    }
}
