using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.Services.PersistantProgrssService;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Mover.Comand
{
    public class RemoveChestCommand : MoveCommand
    {
        private readonly Vector2Int _cheastPosition;

        public RemoveChestCommand(
            IWorldChanger world,
            IMapData mapData,
            Vector2Int cheastPosition,
            NextBuildingForPlacingCreator nextBuildingForPlacingCreator,
            IPersistantProgrss persistentProgressService)
            : base(world, mapData, nextBuildingForPlacingCreator, persistentProgressService)
        {
            _cheastPosition = cheastPosition;
        }

        public override void Execute()
        {
            WorldChanger.RemoveBuilding(_cheastPosition);
            base.Execute();
        }
    }
}
