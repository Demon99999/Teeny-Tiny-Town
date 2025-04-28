using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.GameLogic.Map.StateMachineMap;
using Assets.Scripts.GameLogic.Mover.Comand;
using Assets.Scripts.Services.Input;
using Assets.Scripts.Services.PersistantProgrssService;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Mover
{
    public class ExpandingGameplayMover : CurrencyGameplayMover, IExpandingGameplayMover
    {
        private readonly IExpandingWorldChanger _expandingWorldChanger;

        public ExpandingGameplayMover(
            IExpandingWorldChanger expandingWorldChanger,
            IInputService inputService,
            ICurrencyMapData mapData,
            IPersistantProgrss persistentProgressService,
            WorldStateMachine worldStateMachine,
            NextBuildingForPlacingCreator nextBuildingForPlacingCreator)
            : base(expandingWorldChanger, inputService, mapData, persistentProgressService, worldStateMachine, nextBuildingForPlacingCreator)
        {
            _expandingWorldChanger = expandingWorldChanger;
        }

        public void ExpandWorld(Vector2Int targetSize)
        {
            ExecuteCommand(new ExpandWorldCommand(
                _expandingWorldChanger,
                MapData,
                targetSize,
                LastCommand,
                NextBuildingForPlacingCreator));
        }
    }
}
