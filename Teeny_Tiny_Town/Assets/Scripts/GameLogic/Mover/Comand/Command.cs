using System.Linq;
using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure;

namespace Assets.Scripts.GameLogic.Mover.Comand
{
    public abstract class Command
    {
        protected readonly IWorldChanger WorldChanger;
        protected readonly TileData[] TileDatas;
        protected readonly IMapData WorldData;

        private readonly NextBuildingForPlacingCreator _nextBuildingForPlacingCreator;
        private readonly BuildingsForPlacingData _buildingsForPlacingData;

        public Command(IWorldChanger worldChanger, IMapData worldData, NextBuildingForPlacingCreator nextBuildingForPlacingCreator)
        {
            WorldChanger = worldChanger;
            WorldData = worldData;
            _nextBuildingForPlacingCreator = nextBuildingForPlacingCreator;

            BuildingsForPlacingData buildingsForPlacingData = _nextBuildingForPlacingCreator.BuildingsForPlacingData;

            _buildingsForPlacingData = new BuildingsForPlacingData(
                buildingsForPlacingData.StartGridPosition,
                buildingsForPlacingData.CurrentBuildingType,
                buildingsForPlacingData.NextBuildingType);

            TileDatas = WorldData.Tiles.Select(tile => new TileData(tile.GridPosition, tile.BuildingType)).ToArray();
        }

        public abstract void Execute();

        public virtual void Undo()
        {
            WorldData.UpdateTileDatas(TileDatas);
            WorldChanger.Update(true);
            _nextBuildingForPlacingCreator.Update(_buildingsForPlacingData);
        }
    }
}
