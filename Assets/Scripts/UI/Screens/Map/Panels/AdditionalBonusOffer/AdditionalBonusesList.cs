using Assets.Scripts.Data.Map;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.AdditionalBonuses;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map.Panels.AdditionalBonusOffer
{
    public class AdditionalBonusesList : MonoBehaviour
    {
        private IStaticDataService _staticDataService;
        private IUIFactory _uiFactory;

        [Inject]
        private void Construct(IStaticDataService staticDataService, IUIFactory uiFactory, IMapData worldData)
        {
            _staticDataService = staticDataService;
            _uiFactory = uiFactory;

            foreach (AdditionalBonusType additionalBonusType in _staticDataService.GetMap<MapConfig>(worldData.Id)
                .AvailableAdditionalBonuses)
            {
                _uiFactory.CreateAdditionBonusOfferItem(additionalBonusType, transform);
            }
        }
    }
}
