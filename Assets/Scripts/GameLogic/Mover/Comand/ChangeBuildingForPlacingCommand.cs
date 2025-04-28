using Assets.Scripts.Data;
using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;

namespace Assets.Scripts.GameLogic.Mover.Comand
{
    public class ChangeBuildingForPlacingCommand : Command
    {
        private readonly BuildingType _targetBuilding;
        private readonly uint _buildingPrice;
        private readonly WorldWallet _worldWallet;
        private readonly NextBuildingForPlacingCreator _nextBuildingForPlacingCreator;

        public ChangeBuildingForPlacingCommand(
            IWorldChanger world,
            IMapData worldData,
            BuildingType targetBuilding,
            uint buildingPrice,
            WorldWallet worldWallet,
            NextBuildingForPlacingCreator nextBuildingForPlacingCreator)
            : base(world, worldData, nextBuildingForPlacingCreator)
        {
            _targetBuilding = targetBuilding;
            _buildingPrice = buildingPrice;
            _worldWallet = worldWallet;
            _nextBuildingForPlacingCreator = nextBuildingForPlacingCreator;
        }

        public override void Execute()
        {
            _nextBuildingForPlacingCreator.ChangeCurrentBuildingForPlacing(_targetBuilding);
        }

        public override void Undo()
        {
            _worldWallet.Give(_buildingPrice);
            base.Undo();
        }
    }
}
