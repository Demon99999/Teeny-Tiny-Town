using System;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;

namespace Assets.Scripts.Data.Map
{
    [Serializable]
    public class TileData
    {
        public Vector2Int GridPosition;
        public BuildingType BuildingType;

        public TileData(Vector2Int gridPosition, BuildingType buildingType)
        {
            GridPosition = gridPosition;
            BuildingType = buildingType;
        }
    }
}