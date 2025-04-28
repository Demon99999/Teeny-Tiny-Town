using System;

namespace Assets.Scripts.Services.StaticDataServices.Configs.Buildings
{
    [Serializable]
    public class BuildingUpgradeBranchElement
    {
        public BuildingType BuildingType;
        public BuildingType NextBuildingType;
    }
}