using System;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class BuildingStoreItemData
    {
        public BuildingType Type;
        public uint BuyingCount;

        public BuildingStoreItemData(BuildingType type)
        {
            Type = type;

            BuyingCount = 0;
        }

        public event Action BuyingCountChanged;

        public void ChangeBuyingCount()
        {
            BuyingCount++;
            BuyingCountChanged?.Invoke();
        }
    }
}