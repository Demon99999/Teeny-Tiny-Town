using System;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs.Maps
{
    [Serializable]
    public class TileConfig
    {
        public TileType Type;
        public Vector2Int GridPosition;
        public BuildingType BuildingType;

        public TileConfig(Vector2Int gridPosition)
        {
            GridPosition = gridPosition;
        }
    }
}