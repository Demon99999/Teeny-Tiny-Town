using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.GameLogic.Map.Infrastructure.Buildings;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic.SandBox
{
    public class SandboxChanger : ICenterChangeable, IDisposable
    {
        private readonly IPersistantProgrss _persistentProgressService;
        private readonly IStaticDataService _staticDataService;

        private List<SandboxTile> _tiles;
        private bool _isTileChangedComplete;

        [Inject]
        public SandboxChanger(IPersistantProgrss persistentProgressService, IStaticDataService staticDataService)
        {
            _persistentProgressService = persistentProgressService;
            _staticDataService = staticDataService;

            _isTileChangedComplete = true;
        }

        public event Action<Vector2Int, bool> CenterChanged;

        public void Dispose()
        {
            if (_tiles != null)
            {
                foreach (SandboxTile tile in _tiles)
                {
                    tile.DisposeBuilding();
                }
            }
        }

        public void PutGround(Vector2Int gridPosition, SandboxGroundType type)
        {
            if (_isTileChangedComplete == false)
            {
                return;
            }

            _isTileChangedComplete = false;

            SandboxTile tile = GetTile(gridPosition);
            tile.PutGround(type);

            _isTileChangedComplete = true;
        }

        public void ClearTile(Vector2Int gridPosition)
        {
            if (_isTileChangedComplete == false)
            {
                return;
            }

            _isTileChangedComplete = false;

            GetTile(gridPosition).CleanAll();

            _isTileChangedComplete = true;
        }

        public void Generate(ITileRepresentationCreatable tileRepresentationCreatable)
        {
            Fill(tileRepresentationCreatable);
            CenterChanged?.Invoke(_staticDataService.SandboxConfig.Size, false);
        }

        public void PutBuilding(Vector2Int gridPosition, BuildingType buildingType)
        {
            if (_isTileChangedComplete == false)
            {
                return;
            }

            _isTileChangedComplete = false;

            GetTile(gridPosition).PutBuilding(new Building(buildingType));

            _isTileChangedComplete = true;
        }

        protected void Fill(ITileRepresentationCreatable tileRepresentationCreatable)
        {
            _tiles = CreateTiles();

            InitializeAdjacentTiles(_tiles);

            foreach (SandboxTile tile in _tiles)
            {
                tile.CreateRepresentation(tileRepresentationCreatable);
            }
        }

        private void InitializeAdjacentTiles(List<SandboxTile> tiles)
        {
            foreach (SandboxTile tile in tiles)
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

        private void TryAddNeighborTile(Vector2Int gridPosition, SandboxTile tile)
        {
            SandboxTile adjacentTile = GetTile(gridPosition);

            if (adjacentTile != null && tile.GridPosition != adjacentTile.GridPosition)
            {
                tile.AddAdjacentTile(adjacentTile);
            }
        }

        private IEnumerable<int> GetLineNeighbors(int linePosition)
        {
            for (int i = linePosition - 1; i <= linePosition + 1; i++)
                yield return i;
        }

        private List<SandboxTile> CreateTiles()
        {
            List<SandboxTile> tiles = new List<SandboxTile>();

            foreach (SandboxTileData tileData in _persistentProgressService.Progress.SandboxData.Tiles)
            {
                tiles.Add(new SandboxTile(tileData, _staticDataService));
            }

            return tiles;
        }

        private SandboxTile GetTile(Vector2Int gridPosition)
        {
            return _tiles.FirstOrDefault(tile => tile.GridPosition == gridPosition);
        }
    }
}