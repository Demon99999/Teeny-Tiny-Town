using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Mover.Comand
{
    public class ReplaceBuildingCommand : MoveCommand
    {
        private readonly Vector2Int _fromBuildingGridPosition;
        private readonly BuildingType _fromBuildingType;
        private readonly Vector2Int _toBuildingGridPosition;
        private readonly BuildingType _toBuildingType;
        private readonly uint _replaceItemsCount;

        public ReplaceBuildingCommand(
            IWorldChanger world,
            IMapData mapData,
            Vector2Int fromBuildingGridPosition,
            BuildingType fromBuildingType,
            Vector2Int toBuildingGridPosition,
            BuildingType toBuildingType,
            NextBuildingForPlacingCreator nextBuildingForPlacingCreator,
            IPersistantProgrss persistentProgressService)
            : base(world, mapData, nextBuildingForPlacingCreator, persistentProgressService)
        {
            _fromBuildingGridPosition = fromBuildingGridPosition;
            _fromBuildingType = fromBuildingType;
            _toBuildingGridPosition = toBuildingGridPosition;
            _toBuildingType = toBuildingType;

            _replaceItemsCount = mapData.ReplaceItems.Count;
        }

        public override void Execute()
        {
            WorldData.ReplaceItems.TryGet();
            WorldChanger.ReplaceBuilding(_fromBuildingGridPosition, _fromBuildingType, _toBuildingGridPosition, _toBuildingType);
            base.Execute();
        }

        public override void Undo()
        {
            base.Undo();
            WorldData.ReplaceItems.SetItemsCount(_replaceItemsCount);
        }
    }
}
