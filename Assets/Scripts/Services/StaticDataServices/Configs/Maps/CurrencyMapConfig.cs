using System.Linq;
using Assets.Scripts.Data.Map;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using Assets.Scripts.Services.StaticDataServices.Configs.Store;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs.Maps
{
    [CreateAssetMenu(fileName = "CurrencyMapConfig", menuName = "StaticData/MapConfig/Create new currency map config", order = 51)]
    public class CurrencyMapConfig : MapConfig
    {
        protected const uint StartWorldWalletValue = 20000;

        [SerializeField] public BuildingType[] _startStoreList;
        [SerializeField] public GainStoreItemType[] _availableGainStoreItems;

        public BuildingType[] StartStoreList
        {
            get => _startStoreList;
            set => _startStoreList = value;
        }

        public GainStoreItemType[] AvailableGainStoreItems
        {
            get => _availableGainStoreItems;
            set => _availableGainStoreItems = value;
        }

        public override MapData GetWorldData(uint[] goals, IStaticDataService staticDataService)
        {
            return new CurrencyMapData(Id, TilesDatas, NextBuildingTypeForCreation, StartingAvailableBuildingTypes.ToList(), Size, StartStoreList, goals, GetGainStoreItemsList(staticDataService), IsUnlockedOnStart, StartWorldWalletValue);
        }

        protected GainStoreItemData[] GetGainStoreItemsList(IStaticDataService staticDataService)
        {
            GainStoreItemData[] datas = new GainStoreItemData[AvailableGainStoreItems.Length];

            for (int i = 0; i < AvailableGainStoreItems.Length; i++)
                datas[i] = staticDataService.GetGainStoreItem(AvailableGainStoreItems[i]).GetData();

            return datas;
        }
    }
}
