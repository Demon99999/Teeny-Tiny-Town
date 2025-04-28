using System;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;

namespace Assets.Scripts.Services.StaticDataServices.Configs.Quest
{
    [Serializable]
    public class QuestConfig
    {
        public string Id;
        public uint Reward;
        public string Info;
        public BuildingType BuildingType;
        public uint TargetCount;
    }
}