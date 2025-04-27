using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure.Buildings;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Map.Infrastructure
{
    public class CurrencyWorldChanger : WorldChanger
    {
        private readonly ICurrencyMapData _currencyMapData;

        public CurrencyWorldChanger(
            IStaticDataService staticDataService,
            ICurrencyMapData mapData,
            NextBuildingForPlacingCreator nextBuildingForPlacingCreator,
            IPersistantProgrss persistentProgressService)
            : base(staticDataService, mapData, nextBuildingForPlacingCreator, persistentProgressService) =>
            _currencyMapData = mapData;

        public override Building GetBuilding(BuildingType type, Vector2Int gridPosition)
        {
            switch (type)
            {
                case BuildingType.Undefined:
                    return null;
                case BuildingType.Bush:
                    return new Building(type);
                case BuildingType.Tree:
                    return new Building(type);
                case BuildingType.WoodenHouse:
                    return new PayableBuilding(type, StaticDataService, _currencyMapData.WorldWallet, _currencyMapData);
                case BuildingType.Stone:
                    return new Building(type);
                case BuildingType.Lighthouse:
                    return new Lighthouse(type, _currencyMapData.WorldWallet, _currencyMapData, this, gridPosition);
                case BuildingType.Crane:
                    return new Crane(type, this, gridPosition);
                case BuildingType.Sawmill:
                    return new Building(type);
                case BuildingType.Logs:
                    return new Building(type);
                case BuildingType.BigApartment:
                    return new PayableBuilding(type, StaticDataService, _currencyMapData.WorldWallet, _currencyMapData);
                case BuildingType.FuturisticBuilding:
                    return new PayableBuilding(type, StaticDataService, _currencyMapData.WorldWallet, _currencyMapData);
                case BuildingType.Skyscraper:
                    return new PayableBuilding(type, StaticDataService, _currencyMapData.WorldWallet, _currencyMapData);
                case BuildingType.PileOftones:
                    return new Building(type);
                case BuildingType.Gold:
                    return new Building(type);
                default:
                    return null;
            }
        }
    }
}
