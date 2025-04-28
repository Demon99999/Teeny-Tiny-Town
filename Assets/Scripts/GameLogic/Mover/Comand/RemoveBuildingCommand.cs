using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.Services.PersistantProgrssService;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Mover.Comand
{
    public class RemoveBuildingCommand : MoveCommand
    {
        private readonly Vector2Int _removedBuildingGridPosition;
        private readonly uint _bulldozerItemsCount;

        public RemoveBuildingCommand(
            IWorldChanger world,
            IMapData mapData,
            Vector2Int removedBuildingGridPosition,
            NextBuildingForPlacingCreator nextBuildingForPlacingCreator,
            IPersistantProgrss persistentProgressService)
            : base(world, mapData, nextBuildingForPlacingCreator, persistentProgressService)
        {
            _removedBuildingGridPosition = removedBuildingGridPosition;
            _bulldozerItemsCount = WorldData.BulldozerItems.Count;
        }

        public override void Execute()
        {
            WorldData.BulldozerItems.TryGet();
            WorldChanger.RemoveBuilding(_removedBuildingGridPosition);
            base.Execute();
        }

        public override void Undo()
        {
            base.Undo();
            WorldData.BulldozerItems.SetItemsCount(_bulldozerItemsCount);
        }
    }
}
