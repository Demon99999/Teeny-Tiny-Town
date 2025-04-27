using Assets.Scripts.Audio;
using Assets.Scripts.Camera;
using Assets.Scripts.GameLogic.Map;
using Assets.Scripts.GameLogic.StateMashine;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Infrastructure.StateMachine;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Screens.Map.Panels;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private MapSwitcher _mapSwitcher;
        [SerializeField] private GamePlane _gamePlane;
        [SerializeField] private GameplayCamera _gameplayCamera;
        [SerializeField] private Blur _blur;
        [SerializeField] private UiSoundPlayer _uiSoundPlayer;
        [SerializeField] private WorldWalletSoundPlayer _worldWalletSoundPlayer;
        [SerializeField] protected RemainingMovesPanel RemainingMovesPanel;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameplayBootstrapper>().AsSingle().NonLazy();
            Container.Bind<StatesFactory>().AsSingle();
            Container.Bind<GameplayStateMachine>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();

            Container.BindFactory<MapSwitcher, MapSwitcher.Factory>()
                .FromComponentInNewPrefab(_mapSwitcher.gameObject);

            Container.BindInterfacesAndSelfTo<ScreensSwitcher>().AsSingle();
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();

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

            Container.BindInterfacesAndSelfTo<AudioMixerChanger>().AsSingle().NonLazy();

            Container.BindFactory<RemainingMovesPanel, RemainingMovesPanel.Factory>()
                .FromComponentInNewPrefab(RemainingMovesPanel);
        }
    }
}