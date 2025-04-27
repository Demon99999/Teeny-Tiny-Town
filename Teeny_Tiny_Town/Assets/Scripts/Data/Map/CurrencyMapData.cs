using System;
using System.Collections.Generic;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using Assets.Scripts.Services.StaticDataServices.Configs.Store;
using UnityEngine;

namespace Assets.Scripts.Data.Map
{
    [Serializable]
    public class CurrencyMapData : MapData, ICurrencyMapData
    {
        private int _buildingsIncome;
        private int _lighthouseBonuses;

        public WorldWallet WorldWallet;
        public WorldMovesCounterData MovesCounter;
        public WorldStore WorldStore;
        
        public BuildingType[] StartBuildingsStoreList;

        public CurrencyMapData(
            string id,
            TileData[] tiles,
            BuildingType nextBuildingTypeForCreation,
            List<BuildingType> availableBuildingForCreation,
            Vector2Int size,
            BuildingType[] startBuildingsStoreList,
            uint[] goals,
            GainStoreItemData[] gainsStoreList,
            bool isUnlocked,
            uint startWorldWalletValue)
            : base(id, tiles, nextBuildingTypeForCreation, availableBuildingForCreation, size, goals, isUnlocked)
        {
            WorldWallet = new WorldWallet(startWorldWalletValue);
            MovesCounter = new WorldMovesCounterData();
            WorldStore = new WorldStore(startBuildingsStoreList, gainsStoreList);
            StartBuildingsStoreList = startBuildingsStoreList;
        }

        WorldWallet ICurrencyMapData.WorldWallet => WorldWallet;
        WorldMovesCounterData ICurrencyMapData.MovesCounter => MovesCounter;
        WorldStore ICurrencyMapData.WorldStore => WorldStore;

        protected override void AddNextBuildingTypeForCreation(BuildingType type)
        {
            base.AddNextBuildingTypeForCreation(type);
            WorldStore.TryAddBuilding(type);
        }

        public override void Update(TileData[] tiles, BuildingType nextBuildingTypeForCreation, List<BuildingType> availableBuildingsForCreation)
        {
            base.Update(tiles, nextBuildingTypeForCreation, availableBuildingsForCreation);

            WorldWallet.Clear();
            MovesCounter.MovesCount = 0;
            WorldStore.Clear(StartBuildingsStoreList);
            _buildingsIncome = 0;
            _lighthouseBonuses = 0;
        }

        public void AddBuildingIncome(uint amount) => _buildingsIncome += (int)amount;

        public void AddLighthouseBonus(uint amount) => _lighthouseBonuses += (int)amount;

        public void RemoveBuildingIncome(uint amount) => _buildingsIncome -= (int)amount;

        public void RemoveLighthouseBonus(uint amount) => _lighthouseBonuses -= (int)amount;

        public int GetTotalIncome()
        {
            int payment = _buildingsIncome + _lighthouseBonuses;
            Debug.Log(payment);

            if (payment <= 0)
            {
                payment = 0;
            }

            return payment;
        }
    }
}