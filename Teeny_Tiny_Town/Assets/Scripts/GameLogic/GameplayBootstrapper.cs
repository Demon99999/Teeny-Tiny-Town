using Assets.Scripts.GameLogic.Map;
using Assets.Scripts.GameLogic.StateMashine;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Infrastructure.StateMachine;
using Assets.Scripts.Services.StaticDataServices.Configs.Screens;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Screens;
using Assets.Scripts.UI.Screens.ScreenStart;
using Assets.Scripts.UI.Screens.SelectionMap;
using Zenject;

namespace Assets.Scripts.GameLogic
{
    public class GameplayBootstrapper : IInitializable
    {
        private readonly GameplayStateMachine _gameplayStateMachine;
        private readonly StatesFactory _statesFactory;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly ScreensSwitcher _screensSwitcher;

        public GameplayBootstrapper(GameplayStateMachine stateMachine, StatesFactory statesFactory, IGameFactory gameFactory, ScreensSwitcher screensSwitcher, IUIFactory uiFactory)
        {
            _gameplayStateMachine = stateMachine;
            _statesFactory = statesFactory;
            _gameFactory = gameFactory;
            _screensSwitcher = screensSwitcher;
            _uiFactory = uiFactory;
        }

        public void Initialize()
        {
            _gameFactory.CreatePlane();
            _gameFactory.CreateUiSoundPlayer();
            _gameFactory.CreateCamera();

            RegisterMaps();
            RegisterGameplayStates();
            ;
            _uiFactory.CreateBlur();
            RegisterScreens();

            _gameplayStateMachine.Enter<GameStartState>();
        }

        private void RegisterGameplayStates()
        {
            _gameplayStateMachine.RegisterState(_statesFactory.Create<GameplayLoopState>());
            _gameplayStateMachine.RegisterState(_statesFactory.Create<GameStartState>());
            _gameplayStateMachine.RegisterState(_statesFactory.Create<MapSelectionState>());
            _gameplayStateMachine.RegisterState(_statesFactory.Create<ShowQuestsState>());
        }

        private void RegisterScreens()
        {
            _screensSwitcher.RegisterScreen<MapSelectionScreen>(ScreenType.MapSelection, _uiFactory);
            _screensSwitcher.RegisterScreen<StartScreen>(ScreenType.Start, _uiFactory);
            _screensSwitcher.RegisterScreen<GameplayQuestsWindow>(ScreenType.GameplayQuests, _uiFactory);
        }

        private void RegisterMaps()
        {
            MapSwitcher mapSwitcher = _gameFactory.CreateMapSwitcher();
            mapSwitcher.Initialize();
        }
    }
}