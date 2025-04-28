using Assets.Scripts.Data;
using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Mover.Comand
{
    public class OpenChestCommand : Command
    {
        private readonly uint _reward;
        private readonly Vector2Int _chestGridPosition;
        private readonly WorldWallet _worldWallet;

        public OpenChestCommand(
            IWorldChanger world,
            IMapData mapData,
            uint reward,
            Vector2Int chestGridPosition,
            WorldWallet worldWallet,
            NextBuildingForPlacingCreator nextBuildingForPlacingCreator)
            : base(world, mapData, nextBuildingForPlacingCreator)
        {
            _reward = reward;
            _chestGridPosition = chestGridPosition;
            _worldWallet = worldWallet;
        }

        public override void Execute()
        {
            _worldWallet.Give(_reward);
            WorldChanger.RemoveBuilding(_chestGridPosition);
        }

        public override void Undo()
        {
            _worldWallet.ForceGet(_reward);
            base.Undo();
        }
    }
}
