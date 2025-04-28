using System.Collections.Generic;
using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure.Buildings;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;

namespace Assets.Scripts.GameLogic.Map.Infrastructure
{
    public class RoadTile : TallTile
    {
        private List<RoadTile> _aroundTiles;

        public RoadTile(
            TileData tileData,
            TileType type,
            IStaticDataService staticDataService,
            Building building,
            IMapData mapData,
            IBuildingGivable buildingGivable,
            IPersistantProgrss persistentProgressService)
            : base(tileData, type, staticDataService, building, mapData, buildingGivable, persistentProgressService)
        {
            Ground = new Ground(StaticDataService, StaticDataService.GetGroundType(BuildingType));
            _aroundTiles = new List<RoadTile>();
        }

        public Ground Ground { get; }

        public void ValidateRoadType()
        {
            Ground.TryValidateRoad(GetAdjacentTiles<RoadTile>(), IsEmpty, GridPosition);
        }

        public void ValidateGroundType()
        {
            Ground.TryTakeAroundTilesGroundType(_aroundTiles, IsEmpty);
        }

        public void AddAroundTile(RoadTile aroundTile)
        {
            _aroundTiles.Add(aroundTile);
        }

        public void ChangeRoadsInChain(List<RoadTile> countedTiles, bool isWaitedForCreation)
        {
            countedTiles.Add(this);

            if (Ground.TryValidateRoad(GetAdjacentTiles<RoadTile>(), IsEmpty, GridPosition) == false)
            {
                return;
            }

            foreach (RoadTile tile in GetAdjacentTiles<RoadTile>())
            {
                if (tile.IsEmpty && countedTiles.Contains(tile) == false)
                {
                    tile.ChangeRoadsInChain(countedTiles, isWaitedForCreation);
                }
            }

            CreateGroundRepresentation(isWaitedForCreation);
        }

        public void ChangeGroundsInChain(List<RoadTile> countedTiles, bool isSelfTile = false)
        {
            countedTiles.Add(this);

            if (Ground.TryTakeAroundTilesGroundType(_aroundTiles, IsEmpty) == false && isSelfTile == false)
            {
                return;
            }

            foreach (RoadTile tile in _aroundTiles)
            {
                if (tile.IsEmpty && countedTiles.Contains(tile) == false)
                {
                    tile.ChangeGroundsInChain(countedTiles);
                }
            }
        }

        protected override void CreateGroundRepresentation(bool isWaitedForCreation)
        {
            TileRepresentation.GroundCreator.Create(Ground.Type, Ground.RoadType, Ground.Rotation, isWaitedForCreation);
        }

        protected override void CreateBuildingRepresentation(Building building)
        {
            SetBuilding(building);
            ValidateTilesInChain(false);
            Building.CreateRepresentation(TileRepresentation, true, true);
        }

        private void ValidateTilesInChain(bool isWaitedForRoadCreation)
        {
            if (Ground.TryUpdate(BuildingType))
            {
                ChangeGroundsInChain(new List<RoadTile>(), true);
            }

            ChangeRoadsInChain(new List<RoadTile>(), isWaitedForRoadCreation);
        }

        public override void CleanAll(bool isAnimate)
        {
            base.CleanAll(isAnimate);

            Ground.Clean();
            TileRepresentation.GroundCreator.Create(Ground.Type, Ground.RoadType, Ground.Rotation, isAnimate);
        }

        public override void UpdateBuilding(Building building, IBuildingsUpdatable buildingsUpdatable, bool isAnimate)
        {
            if (building == null)
            {
                return;
            }

            SetBuilding(building);
            Building.CreateRepresentation(TileRepresentation, true, false);

            buildingsUpdatable.UpdateFinished += () => ValidateTilesInChain(true);
        }

        protected override void Clean()
        {
            base.Clean();

            Ground.SetEmpty(_aroundTiles);
            ChangeGroundsInChain(new List<RoadTile>(), true);
            ChangeRoadsInChain(new List<RoadTile>(), false);
        }
    }
}