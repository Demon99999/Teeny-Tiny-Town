using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure.Buildings;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Map.Infrastructure
{
    public class WorldChanger : IWorldChanger, IBuildingsUpdatable, ICenterChangeable, IDisposable
    {
        protected readonly IMapData WorldData;
        protected readonly IStaticDataService StaticDataService;
        protected readonly NextBuildingForPlacingCreator NextBuildingForPlacingCreator;
        private IPersistantProgrss _persistentProgressService;

        private List<Tile> _tiles;

        public WorldChanger(
            IStaticDataService staticDataService,
            IMapData worldData,
            NextBuildingForPlacingCreator nextBuildingForPlacingCreator,
            IPersistantProgrss persistentProgressService)
        {
            StaticDataService = staticDataService;
            WorldData = worldData;
            NextBuildingForPlacingCreator = nextBuildingForPlacingCreator;
            _persistentProgressService = persistentProgressService;
        }

        public event Action TilesChanged;
        public event Action UpdateFinished;
        public event Action<BuildingType> BuildingPlaced;
        public event Action<Vector2Int, bool> CenterChanged;

        public List<Tile> Tiles => _tiles;

        public void Dispose()
        {
            if (_tiles != null)
            {
                foreach (Tile tile in _tiles)
                {
                    tile.DisposeBuilding();
                }
            }
        }

        public void Start()
        {
            TilesChanged?.Invoke();
            OnCenterChanged(false);
        }

        public virtual void Generate(ITileRepresentationCreatable tileRepresentationCreatable)
        {
            Fill(tileRepresentationCreatable);
        }

        public void PlaceNewBuilding(Vector2Int gridPosition, BuildingType buildingType)
        {
            Tile changedTile = GetTile(gridPosition);
            changedTile.PutBuilding(GetBuilding(buildingType, gridPosition));

            NextBuildingForPlacingCreator.MoveToNextBuilding(Tiles);
            TilesChanged?.Invoke();
            BuildingPlaced?.Invoke(buildingType);
        }

        public void Update(bool isAnimate)
        {
            TileData[] tileDatas = new TileData[WorldData.Tiles.Count];

            for (int i = 0; i < WorldData.Tiles.Count; i++)
            {
                tileDatas[i] = new TileData(WorldData.Tiles[i].GridPosition, WorldData.Tiles[i].BuildingType);
            }

            foreach (TileData tileData in tileDatas)
            {
                if (IsTileFitsIntoGrid(tileData.GridPosition) == false)
                {
                    continue;
                }

                Tile tile = GetTile(tileData.GridPosition);

                tile.CleanAll(isAnimate);
            }

            tileDatas = tileDatas.Reverse().ToArray();

            foreach (TileData tileData in tileDatas)
            {
                if (IsTileFitsIntoGrid(tileData.GridPosition) == false)
                {
                    continue;
                }

                Tile tile = GetTile(tileData.GridPosition);

                tile.UpdateBuilding(GetBuilding(tileData.BuildingType, tileData.GridPosition), this, isAnimate);
            }

            UpdateFinished?.Invoke();
            TilesChanged?.Invoke();
        }

        public void ReplaceBuilding(Vector2Int fromBuildingGridPosition, BuildingType fromBuildingType, Vector2Int toBuildingGridPosition, BuildingType toBuildingType)
        {
            Tile fromTile = GetTile(fromBuildingGridPosition);
            fromTile.PutBuilding(GetBuilding(toBuildingType, fromBuildingGridPosition));

            Tile toTile = GetTile(toBuildingGridPosition);
            toTile.PutBuilding(GetBuilding(fromBuildingType, toBuildingGridPosition));

            NextBuildingForPlacingCreator.FindTileToPlacing(Tiles);
            TilesChanged?.Invoke();
        }

        public virtual Building GetBuilding(BuildingType type, Vector2Int gridPosition)
        {
            switch (type)
            {
                case BuildingType.Undefined:
                    return null;
                case BuildingType.Bush:
                    return new Building(type);
                case BuildingType.Tree:
                    return new Building(type);
                case BuildingType.WoodenHouse:
                    return new Building(type);
                case BuildingType.Stone:
                    return new Building(type);
                case BuildingType.Lighthouse:
                    return new Building(type);
                case BuildingType.Crane:
                    return new Crane(type, this, gridPosition);
                case BuildingType.Sawmill:
                    return new Building(type);
                case BuildingType.Logs:
                    return new Building(type);
                case BuildingType.BigApartment:
                    return new Building(type);
                case BuildingType.FuturisticBuilding:
                    return new Building(type);
                case BuildingType.Skyscraper:
                    return new Building(type);
                case BuildingType.PileOftones:
                    return new Building(type);
                case BuildingType.Gold:
                    return new Building(type);
                default:
                    return null;
            }
        }

        public void RemoveBuilding(Vector2Int destroyBuildingGridPosition)
        {
            Tile tile = GetTile(destroyBuildingGridPosition);

            if (tile.Building.Type == BuildingType.Undefined)
                Debug.LogError("Can not destroy empty building");

            tile.RemoveBuilding();

            TilesChanged?.Invoke();
        }

        public Tile GetTile(Vector2Int gridPosition)
        {
            return _tiles.FirstOrDefault(tile => tile.GridPosition == gridPosition);
        }

        public IEnumerable<int> GetLineNeighbors(int linePosition)
        {
            for (int i = linePosition - 1; i <= linePosition + 1; i++)
            {
                yield return i;
            }
        }

        protected void Clean()
        {
            foreach (Tile tile in _tiles)
            {
                tile.Destroy();
            }

            _tiles.Clear();
        }

        protected virtual void Fill(ITileRepresentationCreatable tileRepresentationCreatable)
        {
            List<RoadTile> roadTiles = new List<RoadTile>();
            List<TallTile> tallTiles = new List<TallTile>();

            _tiles = CreateTiles(roadTiles, tallTiles);

            InitializeAdjacentTiles(roadTiles);
            InitializeAdjacentTiles(tallTiles);
            InitializeAroundTiles(roadTiles);
            InitializeRoadTiles(roadTiles);

            foreach (Tile tile in _tiles)
            {
                tile.CreateRepresentation(tileRepresentationCreatable);
            }
        }

        protected virtual List<Tile> CreateTiles(List<RoadTile> roadTiles, List<TallTile> tallTiles)
        {
            List<Tile> tiles = new List<Tile>();

            foreach (TileData tileData in WorldData.Tiles)
            {
                if (IsTileFitsIntoGrid(tileData.GridPosition) == false)
                {
                    continue;
                }

                Tile tile = GetGround(roadTiles, tallTiles, tileData);

                tiles.Add(tile);
            }

            return tiles;
        }

        protected bool IsTileFitsIntoGrid(Vector2Int gridPosition)
        {
            return gridPosition.x < WorldData.Size.x && gridPosition.y < WorldData.Size.y;
        }

        protected Tile GetGround(List<RoadTile> roadTiles, List<TallTile> tallTiles, TileData tileData)
        {
            TileType tileType = StaticDataService.GetMap<MapConfig>(WorldData.Id).GetTileType(tileData.GridPosition);

            switch (tileType)
            {
                case TileType.RoadGround:
                    RoadTile roadTile = new RoadTile(
                        tileData,
                        tileType,
                        StaticDataService,
                        GetBuilding(tileData.BuildingType, tileData.GridPosition),
                        WorldData,
                        this,
                        _persistentProgressService);

                    roadTiles.Add(roadTile);

                    return roadTile;
                case TileType.TallGround:
                    TallTile tallTile = new TallTile(
                        tileData,
                        tileType,
                        StaticDataService,
                        GetBuilding(tileData.BuildingType, tileData.GridPosition),
                        WorldData,
                        this,
                        _persistentProgressService);

                    tallTiles.Add(tallTile);

                    return tallTile;
                case TileType.WaterSurface:
                    return new Tile(
                        tileData,
                        tileType,
                        StaticDataService,
                        GetBuilding(tileData.BuildingType, tileData.GridPosition));
                default:
                    return null;
            }
        }

        protected void OnCenterChanged(bool isNeedAnimate)
        {
            CenterChanged?.Invoke(WorldData.Size, isNeedAnimate);
        }

        protected virtual void InitializeAroundTiles(List<RoadTile> roadTiles)
        {
            foreach (RoadTile tile in roadTiles)
            {
                foreach (int positionY in GetLineNeighbors(tile.GridPosition.y))
                {
                    foreach (int positionX in GetLineNeighbors(tile.GridPosition.x))
                    {
                        TryAddAroundTile(new Vector2Int(positionX, positionY), tile);
                    }
                }
            }
        }

        protected virtual void InitializeAdjacentTiles<TTile>(List<TTile> tiles)
            where TTile : TallTile
        {
            foreach (TTile tile in tiles)
            {
                foreach (int positionX in GetLineNeighbors(tile.GridPosition.x))
                {
                    TryAddNeighborTile(new Vector2Int(positionX, tile.GridPosition.y), tile);
                }

                foreach (int positionY in GetLineNeighbors(tile.GridPosition.y))
                {
                    TryAddNeighborTile(new Vector2Int(tile.GridPosition.x, positionY), tile);
                }
            }
        }

        protected virtual void InitializeRoadTiles(List<RoadTile> roadTiles)
        {
            foreach (RoadTile tile in roadTiles)
            {
                tile.ValidateGroundType();
            }

            foreach (RoadTile tile in roadTiles)
            {
                tile.ValidateRoadType();
            }
        }

        protected void TryAddNeighborTile(Vector2Int gridPosition, TallTile tile)
        {
            TallTile adjacentTile = _tiles.FirstOrDefault(value => value.GridPosition == gridPosition) as TallTile;

            if (adjacentTile != null && tile.GridPosition != adjacentTile.GridPosition &&
                tile.Type == adjacentTile.Type)
            {
                tile.AddAdjacentTile(adjacentTile);
            }
        }

        protected void TryAddAroundTile(Vector2Int gridPosition, RoadTile tile)
        {
            RoadTile aroundTile = _tiles.FirstOrDefault(value => value.GridPosition == gridPosition) as RoadTile;

            if (aroundTile != null && tile != aroundTile)
            {
                tile.AddAroundTile(aroundTile);
            }
        }
    }
}