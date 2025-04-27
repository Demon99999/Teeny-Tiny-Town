using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using Assets.Scripts.Services.StaticDataServices.Configs.Store;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class WorldStore
    {
        public GainStoreItemData[] GainStoreList;
        public List<BuildingStoreItemData> BuildingsStoreList;

        public WorldStore(BuildingType[] startBuildingStoreList, GainStoreItemData[] gainsStoreList)
        {
            GainStoreList = gainsStoreList;

            BuildingsStoreList = new List<BuildingStoreItemData>();

            foreach (BuildingType buildingType in startBuildingStoreList)
            {
                TryAddBuilding(buildingType);
            }
        }

        public event Action<BuildingType> BuildingsStoreListUpdated;
        public event Action Cleared;

        public void TryAddBuilding(BuildingType type)
        {
            if (BuildingsStoreList.Any(data => data.Type == type) == false)
            {
                BuildingStoreItemData data = new BuildingStoreItemData(type);
                BuildingsStoreList.Add(data);
                BuildingsStoreListUpdated?.Invoke(type);
            }
        }

        public GainStoreItemData GetGainData(GainStoreItemType type)
        {
            return GainStoreList.First(data => data.Type == type);
        }

        public BuildingStoreItemData GetBuildingData(BuildingType type)
        {
            return BuildingsStoreList.First(data => data.Type == type);
        }

        public void Clear(BuildingType[] startBuildingsStoreList)
        {
            BuildingsStoreList.Clear();

            foreach (BuildingType buildingType in startBuildingsStoreList)
            {
                TryAddBuilding(buildingType);
            }

            foreach (GainStoreItemData gainStoreItemData in GainStoreList)
            {
                gainStoreItemData.Clear();
            }

            Cleared?.Invoke();
        }
    }
}