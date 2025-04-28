using System;
using Assets.Scripts.Data.Map;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class SandboxTileData : TileData
    {
        public SandboxGroundType GroundType;

        public SandboxTileData(Vector2Int gridPosition)
            : base(gridPosition, BuildingType.Undefined)
        {
            GroundType = SandboxGroundType.Soil;
        }
    }
}