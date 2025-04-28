using System;
using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Data.Map;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Map.Infrastructure.Buildings
{
    public class Lighthouse : Building, IDisposable
    {
        private readonly WorldWallet _worldWallet;
        private readonly ICurrencyMapData _currencyMapData;
        private readonly ITileGetable _tileGetable;
        private readonly Vector2Int _gridPosition;
        private readonly List<Tile> _aroundTiles;

        private uint _currentBonus;

        public Lighthouse(
            BuildingType type,
            WorldWallet worldWallet,
            ICurrencyMapData currencyMapData,
            ITileGetable tileGetable,
            Vector2Int gridPosition)
            : base(type)
        {
            _worldWallet = worldWallet;
            _currencyMapData = currencyMapData;
            _tileGetable = tileGetable;
            _gridPosition = gridPosition;
            _currentBonus = 0;

            _aroundTiles = GetAroundTiles();
            _currencyMapData.MovesCounter.TimeToPaymentPayableBuildings += OnTimeToPaymentPayableBuildings;
            UpdateBonus();
        }

        public Vector2Int GridPosition => _gridPosition;

        public void Dispose()
        {
            _currencyMapData.RemoveLighthouseBonus(_currentBonus);
            _currencyMapData.MovesCounter.TimeToPaymentPayableBuildings -= OnTimeToPaymentPayableBuildings;
        }

        private void OnTimeToPaymentPayableBuildings()
        {
            uint payment = 0;

            foreach (Tile tile in _aroundTiles)
            {
                if (tile.Building is PayableBuilding payableBuilding)
                {
                    payment += payableBuilding.Payment;
                }
            }

            _worldWallet.Give(payment);
        }

        private void UpdateBonus()
        {
            uint newBonus = CalculateBonus();

            _currentBonus = newBonus;
            _currencyMapData.AddLighthouseBonus(_currentBonus);
        }

        private uint CalculateBonus()
        {
            uint newBonus = 0;

            foreach (Tile tile in _aroundTiles)
            {
                if (tile.Building is PayableBuilding payableBuilding)
                {
                    newBonus += payableBuilding.Payment;
                }
            }

            return newBonus;
        }

        public List<Tile> GetAroundTiles()
        {
            List<Tile> tiles = new List<Tile>();

            foreach (int positionY in _tileGetable.GetLineNeighbors(_gridPosition.y))
            {
                foreach (int positionX in _tileGetable.GetLineNeighbors(_gridPosition.x))
                {
                    Tile tile = _tileGetable.GetTile(new Vector2Int(positionX, positionY));

                    if (tile != null)
                    {
                        tiles.Add(tile);
                        Debug.Log(tile.BuildingType);
                    }
                }
            }

            return tiles;
        }
    }
}
