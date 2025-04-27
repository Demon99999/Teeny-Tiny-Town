using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.GameLogic.Map.Representation.ActionHandler;
using Assets.Scripts.GameLogic.Map.StateMachineMap;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Infrastructure.StateMachine;
using Assets.Scripts.Services.PersistantProgrssService;

namespace Assets.Scripts.GameLogic.Map.Installer
{
    public class CurrencyWorldBootstrapper : MapBootstrapper
    {
        private readonly IGameFactory _gameplayFactory;

        public CurrencyWorldBootstrapper(
            IWorldChanger worldChanger,
            IMapFactory worldFactory,
            WorldStateMachine worldStateMachine,
            StatesFactory statesFactory,
            Map world,
            ActionHandlerStateMachine actionHandlerStateMachine,
            ActionHandlerStatesFactory actionHandlerStatesFactory,
            NextBuildingForPlacingCreator nextBuildingForPlacingCreator,
            IPersistantProgrss persistentProgressService,
            IGameFactory gameplayFactory)
            : base(
                  worldChanger,
                  worldFactory,
                  worldStateMachine,
                  statesFactory,
                  world,
                  actionHandlerStateMachine,
                  actionHandlerStatesFactory,
                  nextBuildingForPlacingCreator,
                  persistentProgressService)
        {
            _gameplayFactory = gameplayFactory;
        }

        public override void Initialize()
        {
            base.Initialize();
            _gameplayFactory.CreateWorldWalletSoundPlayer();
        }

        protected override void RegisterStates(bool needRegisterWaitinState)
        {
            WorldStateMachine.RegisterState(StatesFactory.Create<WorldStartState>());
            WorldStateMachine.RegisterState(StatesFactory.Create<WorldChangingState>());
            WorldStateMachine.RegisterState(StatesFactory.Create<ExitWorldState>());
            WorldStateMachine.RegisterState(StatesFactory.Create<ResultState>());
            WorldStateMachine.RegisterState(StatesFactory.Create<RewardState>());
            WorldStateMachine.RegisterState(StatesFactory.Create<QuestsState>());
            WorldStateMachine.RegisterState(StatesFactory.Create<SafeGameplayState>());
            WorldStateMachine.RegisterState(StatesFactory.Create<StoreState>());
            WorldStateMachine.RegisterState(StatesFactory.Create<GainBuyingState>());

            if (needRegisterWaitinState)
            {
                WorldStateMachine.RegisterState(StatesFactory.Create<WaitingState>());
            }
        }

        protected override void OnMapEntered()
        {
            WorldStateMachine.Enter<WorldStartState>();
        }
    }
}