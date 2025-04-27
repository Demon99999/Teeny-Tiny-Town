using System.Linq;
using Assets.Scripts.Data.Map;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs.Maps
{
    [CreateAssetMenu(fileName = "MapConfig", menuName = "StaticData/MapConfig/Create new expanding map config", order = 51)]
    public class ExpandingMapConfig : CurrencyMapConfig
    {
        [SerializeField] private Vector2Int _startSize;
        [SerializeField] private ExpandConfig[] _expandConfigs;

        public Vector2Int StartSize => _startSize;

        public ExpandConfig[] ExpandConfigs => _expandConfigs;

        public bool ContainsExpand(BuildingType type, out ExpandConfig expandConfig)
        {
            expandConfig = ExpandConfigs.FirstOrDefault(config => config.BuidldingType == type);

            return expandConfig != null;
        }

        public override MapData GetWorldData(uint[] goals, IStaticDataService staticDataService)
        {
            return new CurrencyMapData(Id, TilesDatas, NextBuildingTypeForCreation, StartingAvailableBuildingTypes.ToList(), StartSize, StartStoreList, goals, GetGainStoreItemsList(staticDataService), IsUnlockedOnStart, StartWorldWalletValue);
        }
    }
}