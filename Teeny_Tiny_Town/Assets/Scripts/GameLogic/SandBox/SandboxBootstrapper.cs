using Assets.Scripts.GameLogic.Map;
using Assets.Scripts.GameLogic.Map.Representation.ActionHandler;
using Assets.Scripts.GameLogic.Map.Representation.Markers;
using Assets.Scripts.GameLogic.SandBox.Action;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Services.StaticDataServices.Configs.Screens;
using Assets.Scripts.UI;
using Zenject;

namespace Assets.Scripts.GameLogic.SandBox
{
    public class SandboxBootstrapper : IInitializable
    {
        private readonly IUIFactory _uiFactory;
        private readonly SandboxChanger _sandboxChanger;
        private readonly IMapFactory _worldFactory;
        private readonly ActionHandlerStateMachine _actionHandlerStateMachine;
        private readonly ActionHandlerStatesFactory _actionHandlerStatesFactory;
        private readonly SandboxRotation _sandboxRotation;
        private readonly IGameFactory _gameplayFactory;

        public SandboxBootstrapper(
            IUIFactory uiFactory,
            SandboxChanger sandboxChanger,
            IMapFactory worldFactory,
            ActionHandlerStateMachine actionHandlerStateMachine,
            ActionHandlerStatesFactory actionHandlerStatesFactory,
            SandboxRotation sandboxRotation,
            IGameFactory gameplayFactory)
        {
            _uiFactory = uiFactory;
            _sandboxChanger = sandboxChanger;
            _worldFactory = worldFactory;
            _actionHandlerStateMachine = actionHandlerStateMachine;
            _actionHandlerStatesFactory = actionHandlerStatesFactory;
            _sandboxRotation = sandboxRotation;
            _gameplayFactory = gameplayFactory;
        }

        public void Initialize()
        {
            _gameplayFactory.CreatePlane();
            _gameplayFactory.CreateUiSoundPlayer();
            SandboxWorld sandboxWorld = _worldFactory.CreateSandboxWorld();
            WorldGenerator worldGenerator = _worldFactory.CreateWorldGenerator(sandboxWorld.transform);
            _sandboxRotation.Init(sandboxWorld);
            _sandboxChanger.Generate(worldGenerator);
            SelectFrame selectFrame = _worldFactory.CreateSelectFrame(worldGenerator.transform);
            selectFrame.Hide();

            _actionHandlerStateMachine.RegisterState(_actionHandlerStatesFactory.CreateHandlerState<BuildingPositionHandler>());
            _actionHandlerStateMachine.RegisterState(_actionHandlerStatesFactory.CreateHandlerState<GroundPositionHandler>());
            _actionHandlerStateMachine.RegisterState(_actionHandlerStatesFactory.CreateHandlerState<ClearTilePositionHandler>());
            _actionHandlerStateMachine.RegisterState(_actionHandlerStatesFactory.CreateHandlerState<NothingSelectedState>());

            ScreenBase sandboxWindow = _uiFactory.CreateScreen(ScreenType.Sandbox);

            _actionHandlerStateMachine.Enter<NothingSelectedState>();

            sandboxWindow.Open();
        }
    }
}
