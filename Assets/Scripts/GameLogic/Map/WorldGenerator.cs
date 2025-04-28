using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic.Map
{
    public class WorldGenerator : MonoBehaviour, ITileRepresentationCreatable
    {
        [SerializeField] private float _cellSize;

        private IMapFactory _mapFactory;

        private List<TileRepresentation> _tiles;

        public float CellSize => _cellSize;

        [Inject]
        private void Construct(IMapFactory mapFactory)
        {
            _mapFactory = mapFactory;
            _tiles = new List<TileRepresentation>();
        }

        public TileRepresentation GetTile(Vector2Int gridPosition)
        {
            return _tiles.First(tile => tile.GridPosition == gridPosition);
        }

        public TileRepresentation Create(Vector2Int gridPosition, TileType tileType)
        {
            Vector3 worldPosition = GridToWorldPosition(gridPosition) + transform.position;
            TileRepresentation tileRepresentation = _mapFactory.CreateTileRepresentation(worldPosition, transform);

            tileRepresentation.Init(tileType, gridPosition);

            if (_tiles.Any(value => value.GridPosition == gridPosition))
            {
                _tiles.Remove(GetTile(gridPosition));
            }

            _tiles.Add(tileRepresentation);

            return tileRepresentation;
        }

        private Vector3 GridToWorldPosition(Vector2Int gridPosition)
        {
            return new Vector3(
                gridPosition.x * _cellSize,
                transform.position.y,
                gridPosition.y * _cellSize);
        }

        public class Factory : PlaceholderFactory<WorldGenerator>
        {

        }
    }
}