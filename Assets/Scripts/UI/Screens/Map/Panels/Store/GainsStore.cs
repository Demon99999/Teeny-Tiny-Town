using Assets.Scripts.Data.Map;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;
using Assets.Scripts.Services.StaticDataServices.Configs.Store;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map.Panels.Store
{
    public class GainsStore : MonoBehaviour
    {
        private IUIFactory _uiFactory;
        private ICurrencyMapData _currencyWorldData;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(ICurrencyMapData currencyWorldData, IStaticDataService staticDataService, IUIFactory uiFactory)
        {
            _currencyWorldData = currencyWorldData;
            _staticDataService = staticDataService;
            _uiFactory = uiFactory;

            foreach (GainStoreItemType gainType in _staticDataService.GetMap<CurrencyMapConfig>(_currencyWorldData.Id)
                .AvailableGainStoreItems)
            {
                _uiFactory.CreateGainStoreItemPanel(gainType, transform);
            }
        }
    }
}
