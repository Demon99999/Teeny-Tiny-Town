using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs.Store
{
    [CreateAssetMenu(fileName = "LimitedQuantityGainStorItemConfig", menuName = "StaticData/WorldStore/Create new limited quantity gain store item config", order = 51)]
    public class LimitedQuantityGainStoreItemConfig : GainStoreItemConfig
    {
        [SerializeField] private uint _availableCount;

        public uint AvailableCount => _availableCount;

        public override GainStoreItemData GetData()
        {
            return new GainStoreItemData(Type, AvailableCount);
        }
    }
}
