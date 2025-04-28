using System;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;

namespace Assets.Scripts.Services.StaticDataServices.Configs
{
    [Serializable]
    public class RoadGroundConfig
    {
        public BuildingType BuildingType;
        public GroundType GroundType;
    }
}