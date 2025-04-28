using System.Collections.Generic;
using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure.Buildings;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;

namespace Assets.Scripts.GameLogic.Map.Infrastructure
{
    public class TallTile : Tile
    {
        private const uint MinTilesCountToMerge = 3;

        private readonly IBuildingGivable _buildingGibable;
        private readonly IPersistantProgrss _persistentProgressService;
        private IMapData _mapData;

        private List<TallTile> _adjacentTiles;

        public TallTile(
            TileData tileData,
            TileType type,
            IStaticDataService staticDataService,
            Building building,
            IMapData mapData,
            IBuildingGivable buildingGibable,
            IPersistantProgrss persistentProgressService)
            : base(tileData, type, staticDataService, building)
        {
            _mapData = mapData;
            _buildingGibable = buildingGibable;
            _persistentProgressService = persistentProgressService;

            _adjacentTiles = new List<TallTile>();
        }

        public IReadOnlyList<TallTile> AdjacentTiles => _adjacentTiles;

        public void AddAdjacentTile(TallTile adjacentTile)
        {
            _adjacentTiles.Add(adjacentTile);
        }

        protected override void SetUpBuilding(Building building)
        {
            SetBuilding(building);
            TryUpdateBuildingsChain(building);
        }

        public int GetBuildingsChainLength(List<TallTile> countedTiles, BuildingType targetBuildingType = default)
        {
            int chainLength = 1;
            countedTiles.Add(this);

            foreach (TallTile tile in _adjacentTiles)
            {
                if (targetBuildingType == default)
                {
                    if (BuildingType == tile.BuildingType && countedTiles.Contains(tile) == false)
                    {
                        chainLength += tile.GetBuildingsChainLength(countedTiles);
                    }
                }
                else
                {
                    if (targetBuildingType == tile.BuildingType && countedTiles.Contains(tile) == false)
                    {
                        chainLength += tile.GetBuildingsChainLength(countedTiles);
                    }
                }
            }

            return chainLength;
        }

        public void TryUpdateBuildingsChain(Building building)
        {
            CreateBuildingRepresentation(building);

            if (IsEmpty)
                return;

            bool chainCheckCompleted = false;

            while (chainCheckCompleted == false)
            {
                List<TallTile> countedTiles = new List<TallTile>();

                if (GetBuildingsChainLength(countedTiles) >= MinTilesCountToMerge && TryUpgradeBuilding())
                {
                    List<TallTile> tilesForRemoveBuildings = countedTiles;
                    tilesForRemoveBuildings.Remove(this);

                    foreach (Tile tile in countedTiles)
                    {
                        tile.RemoveBuilding(TileRepresentation.BuildingPoint.position);
                    }

                    _mapData.TryAddBuildingTypeForCreation(
                        BuildingType,
                        StaticDataService.AvailableForConstructionBuildingsConfig.RequiredCreatedBuildingsToAddNext,
                        StaticDataService);
                }
                else
                {
                    chainCheckCompleted = true;
                }
            }
        }

        protected IReadOnlyList<TAdjacentTile> GetAdjacentTiles<TAdjacentTile>()
        {
            List<TAdjacentTile> adjacentTiles = new List<TAdjacentTile>();

            foreach (TallTile tile in _adjacentTiles)
            {
                if (tile is TAdjacentTile adjacentTile)
                {
                    adjacentTiles.Add(adjacentTile);
                }
            }

            return adjacentTiles;
        }

        private bool TryUpgradeBuilding()
        {
            if (StaticDataService.AvailableForConstructionBuildingsConfig.TryFindeNextBuilding(BuildingType, out BuildingType nextBuildingType))
            {
                CreateBuildingRepresentation(_buildingGibable.GetBuilding(nextBuildingType, GridPosition));
                _persistentProgressService.Progress.AddBuildingToCollection(nextBuildingType);

                return true;
            }

            return false;
        }
    }
}