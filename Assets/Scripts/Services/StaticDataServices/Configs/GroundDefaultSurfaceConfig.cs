using System;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;

namespace Assets.Scripts.Services.StaticDataServices.Configs
{
    [Serializable]
    public class GroundDefaultSurfaceConfig
    {
        public TileType TileType;
        public Ground Prefab;
    }
}