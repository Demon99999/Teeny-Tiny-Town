using System;
using Assets.Scripts.Data;
using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Map.Infrastructure.Buildings
{
    [Serializable]
    public class PayableBuilding : Building, IDisposable
    {
        public readonly uint Payment;
        
        private readonly IStaticDataService _staticDataService;
        private readonly WorldWallet _worldWallet;
        private readonly ICurrencyMapData _currencyMapData;
        
        public PayableBuilding(BuildingType type, IStaticDataService staticDataService, WorldWallet worldWallet, ICurrencyMapData currencyMapData)
            : base(type)
        {
            _staticDataService = staticDataService;
            _worldWallet = worldWallet;
            _currencyMapData = currencyMapData;

            Payment = _staticDataService.GetBuilding<PayableBuildingConfig>(Type).Payment;

            _currencyMapData.MovesCounter.TimeToPaymentPayableBuildings += OnTimeToPaymentPayableBuildings;
            _currencyMapData.AddBuildingIncome(Payment);
        }

        public void Dispose()
        {
            _currencyMapData.RemoveBuildingIncome(Payment);
            _currencyMapData.MovesCounter.TimeToPaymentPayableBuildings -= OnTimeToPaymentPayableBuildings;
        }

        private void OnTimeToPaymentPayableBuildings()
        {
            _worldWallet.Give(Payment);
        }
    }
}
