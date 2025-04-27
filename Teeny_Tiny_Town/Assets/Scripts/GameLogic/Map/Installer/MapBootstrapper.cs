using System;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.GameLogic.Map.Representation.ActionHandler;
using Assets.Scripts.GameLogic.Map.StateMachineMap;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Infrastructure.StateMachine;
using Assets.Scripts.Services.PersistantProgrssService;
using Zenject;

namespace Assets.Scripts.GameLogic.Map.Installer
{
    public class MapBootstrapper : IInitializable, IDisposable
    {
        private readonly IWorldChanger _worldChanger;
        private readonly IMapFactory _mapFactory;
        private readonly Map _map;
        private readonly ActionHandlerStateMachine _actionHandlerStateMachine;
        private readonly ActionHandlerStatesFactory _actionHandlerStatesFactory;
        private readonly NextBuildingForPlacingCreator _nextBuildingForPlacingCreator;
        private readonly IPersistantProgrss _persistentProgressService;
        protected readonly WorldStateMachine WorldStateMachine;
        protected readonly StatesFactory StatesFactory;

        public MapBootstrapper(
            IWorldChanger worldChanger, 
            IMapFactory mapFactory,
            WorldStateMachine worldStateMachine,
            StatesFactory statesFactory,
            Map map,
            ActionHandlerStateMachine actionHandlerStateMachine,
            ActionHandlerStatesFactory actionHandlerStatesFactory,
            NextBuildingForPlacingCreator nextBuildingForPlacingCreator,
            IPersistantProgrss persistentProgressService)
        {
            _worldChanger = worldChanger;
            _mapFactory = mapFactory;
            WorldStateMachine = worldStateMachine;
            StatesFactory = statesFactory;
            _map = map;

            _map.Entered += OnMapEntered;
            _actionHandlerStateMachine = actionHandlerStateMachine;
            _actionHandlerStatesFactory = actionHandlerStatesFactory;
            _nextBuildingForPlacingCreator = nextBuildingForPlacingCreator;
            _persistentProgressService = persistentProgressService;
        }

        public void Dispose()
        {
            _map.Entered -= OnMapEntered;
        }

        public virtual void Initialize()
        {
            WorldGenerator worldGenerator = _mapFactory.CreateWorldGenerator();
            _worldChanger.Generate(worldGenerator);
            _mapFactory.CreateSelectFrame(worldGenerator.transform);
            _mapFactory.CreateBuildingMarker(worldGenerator.transform);

            RegisterActionHandlerStates();
            RegisterStates(_persistentProgressService.Progress.StoreData.IsInfinityMovesUnlocked == false);

            _actionHandlerStateMachine.Enter<NewBuildingPlacePositionHandler>();
            _nextBuildingForPlacingCreator.CreateData(_worldChanger.Tiles);
            _worldChanger.Start();
            _map.OnCreated();
        }

        protected virtual void RegisterStates(bool needRegisterWaitinState)
        {
            WorldStateMachine.RegisterState(StatesFactory.Create<WorldStartState>());
            WorldStateMachine.RegisterState(StatesFactory.Create<WorldChangingState>());
            WorldStateMachine.RegisterState(StatesFactory.Create<ExitWorldState>());
            WorldStateMachine.RegisterState(StatesFactory.Create<ResultState>());
            WorldStateMachine.RegisterState(StatesFactory.Create<RewardState>());
            WorldStateMachine.RegisterState(StatesFactory.Create<QuestsState>());
            WorldStateMachine.RegisterState(StatesFactory.Create<SafeGameplayState>());

            if (needRegisterWaitinState)
            {
                WorldStateMachine.RegisterState(StatesFactory.Create<WaitingState>());
            }
        }

        protected virtual void OnMapEntered()
        {
            WorldStateMachine.Enter<WorldStartState>();
        }

        private void RegisterActionHandlerStates()
        {
            _actionHandlerStateMachine.RegisterState(_actionHandlerStatesFactory.CreateHandlerState<NewBuildingPlacePositionHandler>());
            _actionHandlerStateMachine.RegisterState(_actionHandlerStatesFactory.CreateHandlerState<RemovedBuildingPositionHandler>());
            _actionHandlerStateMachine.RegisterState(_actionHandlerStatesFactory.CreateHandlerState<ReplacedBuildingPositionHandler>());
        }
    }
}
