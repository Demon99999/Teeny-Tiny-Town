using Assets.Scripts.Data.Map;
using Assets.Scripts.Services.StaticDataServices.Configs.Store;

namespace Assets.Scripts.UI.Screens.Map.Panels.Store
{
    public class UnlimitedQuantityGainBuyer
    {
        private readonly ICurrencyMapData _currencyMapData;

        public UnlimitedQuantityGainBuyer(ICurrencyMapData currencyMapData)
        {
            _currencyMapData = currencyMapData;
        }

        public GainStoreItemType GainStoreItemType { get; private set; }

        public void SetBuyingGainType(GainStoreItemType type)
        {
            GainStoreItemType = type;
        }

        public void ChangeGainItemsCoutn(uint count)
        {
            switch (GainStoreItemType)
            {
                case GainStoreItemType.ReplaceItems:
                    _currencyMapData.ReplaceItems.AddItems(count);
                    break;
                case GainStoreItemType.Bulldozer:
                    _currencyMapData.BulldozerItems.AddItems(count);
                    break;
            }

            _currencyMapData.WorldStore.GetGainData(GainStoreItemType).ChangeBuyingCount(count);
        }
    }
}
