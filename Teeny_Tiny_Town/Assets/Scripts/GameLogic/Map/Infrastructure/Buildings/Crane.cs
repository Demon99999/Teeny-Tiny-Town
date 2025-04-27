using System.Collections.Generic;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Map.Infrastructure.Buildings
{
    public class Crane : Building
    {
        private const uint MinTilesCountToMerge = 3;

        private readonly ITileGetable _tileGetable;
        private readonly Vector2Int _gridPosition;

        private TallTile _selfTile;

        public Crane(BuildingType type, ITileGetable tileGetable, Vector2Int gridPosition)
            : base(type)
        {
            _tileGetable = tileGetable;
            _gridPosition = gridPosition;

            Type = GetNewType();
        }

        public override void CreateRepresentation(TileRepresentation tileRepresentation, bool isAnimate, bool waitForCompletion)
        {
            base.CreateRepresentation(tileRepresentation, isAnimate, waitForCompletion);

            if (Type == BuildingType.Crane)
            {
                _selfTile.RemoveBuilding();
            }
        }

        private BuildingType GetNewType()
        {
            HashSet<BuildingType> aroundBuildingTypes = new HashSet<BuildingType>();
            _selfTile = _tileGetable.GetTile(_gridPosition) as TallTile;
            BuildingType newType = BuildingType.Undefined;

            foreach (Tile tile in _selfTile.AdjacentTiles)
            {
                if (tile.BuildingType != BuildingType.Undefined)
                    aroundBuildingTypes.Add(tile.BuildingType);
            }

            foreach (BuildingType type in aroundBuildingTypes)
            {
                int chainLength = _selfTile.GetBuildingsChainLength(new List<TallTile>(), type);

                if (chainLength >= MinTilesCountToMerge && (int)type > (int)newType)
                    newType = type;
            }

            return newType == BuildingType.Undefined ? BuildingType.Crane : newType;
        }
    }
}