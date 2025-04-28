using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.GameLogic.Map.Representation.ActionHandler;
using Assets.Scripts.GameLogic.Map.StateMachineMap;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Infrastructure.StateMachine;
using Assets.Scripts.Services.PersistantProgrssService;

namespace Assets.Scripts.GameLogic.Map.Installer
{
    public class EducationWorldBootstrapper : CurrencyWorldBootstrapper
    {
        private const uint GainItemsCount = 1;

        private readonly IMapData _mapData;

        public EducationWorldBootstrapper(
            IWorldChanger worldChanger,
            IMapFactory worldFactory,
            WorldStateMachine worldStateMachine,
            StatesFactory statesFactory,
            Map world,
            ActionHandlerStateMachine actionHandlerStateMachine,
            ActionHandlerStatesFactory actionHandlerStatesFactory,
            NextBuildingForPlacingCreator nextBuildingForPlacingCreator,
            IPersistantProgrss persistentProgressService,
            IMapData mapData,
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
                  persistentProgressService,
                  gameplayFactory)
        {
            _mapData = mapData;
        }

        public override void Initialize()
        {
            _mapData.ReplaceItems.Count = GainItemsCount;
            _mapData.BulldozerItems.Count = GainItemsCount;
            _mapData.IsChangingStarted = true;

            base.Initialize();
        }
    }
}
