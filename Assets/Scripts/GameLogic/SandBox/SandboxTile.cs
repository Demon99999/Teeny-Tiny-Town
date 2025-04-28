using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.GameLogic.Map.Infrastructure.Buildings;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;
using UnityEngine;

namespace Assets.Scripts.GameLogic.SandBox
{
    public class SandboxTile
    {
        public readonly Vector2Int GridPosition;

        private readonly SandboxTileData _tileData;
        private readonly IStaticDataService _staticDataService;

        private List<SandboxTile> _adjacentTiles;

        public SandboxTile(SandboxTileData tileData, IStaticDataService staticDataService)
        {
            _tileData = tileData;
            _staticDataService = staticDataService;

            GridPosition = _tileData.GridPosition;

            _adjacentTiles = new List<SandboxTile>();

            if (tileData.BuildingType != BuildingType.Undefined)
            {
                Building = new Building(_tileData.BuildingType);
            }
        }

        public bool IsEmpty => Building == null;
        public SandboxGroundType SandboxGroundType => _tileData.GroundType;
        public Building Building { get; protected set; }
        protected TileRepresentation TileRepresentation { get; private set; }

        public void AddAdjacentTile(SandboxTile adjacentTile)
        {
            _adjacentTiles.Add(adjacentTile);
        }

        public void CleanAll()
        {
            if (IsEmpty == false)
            {
                TileRepresentation.DestroyBuilding(false);
            }

            SetBuilding(null);

            bool needChangeRoadsInChain = SandboxGroundType == SandboxGroundType.AsphaltRoad || SandboxGroundType == SandboxGroundType.SoilRoad;
            _tileData.GroundType = SandboxGroundType.Soil;
            TileRepresentation.GroundCreator.Create(TileType.RoadGround);

            if (needChangeRoadsInChain)
            {
                foreach (SandboxTile adjacentTile in _adjacentTiles)
                {
                    adjacentTile.ChangeRoadsInChain(new List<SandboxTile>());
                }
            }
        }

        public void PutGround(SandboxGroundType sandboxGroundType)
        {
            RemoveBuilding();

            if (SandboxGroundType == sandboxGroundType)
            {
                return;
            }

            _tileData.GroundType = sandboxGroundType;

            switch (sandboxGroundType)
            {
                case SandboxGroundType.Soil:
                    TileRepresentation.GroundCreator.Create(TileType.RoadGround);
                    break;
                case SandboxGroundType.WaterSurface:
                    TileRepresentation.GroundCreator.Create(TileType.WaterSurface);
                    break;
                case SandboxGroundType.TallGround:
                    TileRepresentation.GroundCreator.Create(TileType.TallGround);
                    break;
                case SandboxGroundType.SoilRoad:
                    ChangeRoadsInChain(new List<SandboxTile>());
                    break;
                case SandboxGroundType.AsphaltRoad:
                    ChangeRoadsInChain(new List<SandboxTile>());
                    break;
            }
        }

        public void ChangeRoadsInChain(List<SandboxTile> countedTiles)
        {
            if (SandboxGroundType != SandboxGroundType.AsphaltRoad && SandboxGroundType != SandboxGroundType.SoilRoad)
            {
                return;
            }

            countedTiles.Add(this);

            TryValidateRoad(_adjacentTiles, IsEmpty, GridPosition);

            foreach (SandboxTile tile in _adjacentTiles)
            {
                if (tile.SandboxGroundType == SandboxGroundType && countedTiles.Contains(tile) == false)
                {
                    tile.ChangeRoadsInChain(countedTiles);
                }
            }
        }

        public void PutBuilding(Building building)
        {
            if (building == null)
            {
                RemoveBuilding();

                return;
            }

            SetUpBuilding(building);

            if (SandboxGroundType == SandboxGroundType.AsphaltRoad || SandboxGroundType == SandboxGroundType.SoilRoad)
            {
                ChangeRoadsInChain(new List<SandboxTile>());
            }

            if (_tileData.GroundType != SandboxGroundType.TallGround && building.Type != BuildingType.Lighthouse)
            {
                GroundType groundType = _staticDataService.GetGroundType(Building.Type);

                TileRepresentation.GroundCreator.Create(groundType, RoadType.NonEmpty, GroundRotation.Degrees0, false);

                _tileData.GroundType = SandboxGroundType.Soil;
            }
            else if (building.Type == BuildingType.Lighthouse)
            {
                TileRepresentation.GroundCreator.Create(TileType.WaterSurface);

                _tileData.GroundType = SandboxGroundType.WaterSurface;
            }
        }

        public void RemoveBuilding()
        {
            if (IsEmpty)
                return;

            TileRepresentation.DestroyBuilding(false);
            Clean();
        }

        public void CreateRepresentation(ITileRepresentationCreatable tileRepresentationCreatable)
        {
            TileRepresentation = tileRepresentationCreatable.Create(GridPosition, GetTileType());
            CreateGroundRepresentation();

            if (Building != null)
            {
                Building.CreateRepresentation(TileRepresentation, false, false);
            }
        }

        public void DisposeBuilding()
        {
            if (Building != null && Building is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        private void TryValidateRoad(IReadOnlyList<SandboxTile> adjacentTiles, bool isEmpty, Vector2Int gridPosition)
        {
            if (isEmpty)
            {
                List<Vector2Int> adjacentEmptyTileGridPositions = adjacentTiles
                    .Where(tile => tile.IsEmpty && tile.SandboxGroundType == SandboxGroundType)
                    .Select(tile => tile.GridPosition)
                    .ToList();

                GroundType groundType = SandboxGroundType == SandboxGroundType.SoilRoad ? GroundType.Soil : GroundType.Asphalt;

                TryChangeRoad(gridPosition, adjacentEmptyTileGridPositions, groundType);
            }
        }

        private void TryChangeRoad(Vector2Int gridPosition, List<Vector2Int> adjacentGridPosition, GroundType targetGroundType)
        {
            RoadType newRoadType = _staticDataService.GroundsConfig.GetRoadType(gridPosition, adjacentGridPosition, targetGroundType, out GroundRotation rotation);

            TileRepresentation.GroundCreator.Create(targetGroundType, newRoadType, rotation, false);
        }

        private TileType GetTileType()
        {
            switch (_tileData.GroundType)
            {
                case SandboxGroundType.Soil:
                    return TileType.RoadGround;
                case SandboxGroundType.WaterSurface:
                    return TileType.WaterSurface;
                case SandboxGroundType.TallGround:
                    return TileType.TallGround;
                case SandboxGroundType.SoilRoad:
                    return TileType.RoadGround;
                case SandboxGroundType.AsphaltRoad:
                    return TileType.RoadGround;
            }

            return TileType.RoadGround;
        }

        protected virtual void CreateGroundRepresentation()
        {
            if (_tileData.GroundType == SandboxGroundType.AsphaltRoad ||
                _tileData.GroundType == SandboxGroundType.SoilRoad)
            {
                TryValidateRoad(_adjacentTiles, IsEmpty, GridPosition);
            }
            else
            {
                TileRepresentation.GroundCreator.Create(GetTileType());
            }
        }

        protected virtual void SetUpBuilding(Building building)
        {
            CreateBuildingRepresentation(building);
        }

        protected void Clean()
        {
            SetBuilding(null);

            _tileData.GroundType = SandboxGroundType.Soil;
        }

        protected virtual void CreateBuildingRepresentation(Building building)
        {
            SetBuilding(building);

            Building.CreateRepresentation(TileRepresentation, false, false);
        }

        protected void SetBuilding(Building building)
        {
            DisposeBuilding();

            Building = building;
            _tileData.BuildingType = IsEmpty ? BuildingType.Undefined : Building.Type;
        }
    }
}
