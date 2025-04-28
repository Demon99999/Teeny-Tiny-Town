using System;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class BuildingData
    {
        public BuildingType Type;
        public uint Count;

        public BuildingData(BuildingType type)
        {
            Type = type;

            Count = 0;
        }

        public bool IsUnlocked => Count > 0;
    }
}