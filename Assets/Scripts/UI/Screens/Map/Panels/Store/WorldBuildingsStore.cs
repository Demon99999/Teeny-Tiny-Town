using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.StateMachineMap;
using Assets.Scripts.GameLogic.Mover;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map.Panels.Store
{
    public class WorldBuildingsStore : MonoBehaviour
    {
        private IUIFactory _uiFactory;
        private ICurrencyMapData _mapData;
        private List<BuildingStoreItem> _storeItems;
        private ICurrencyGameplayMover _gameplayMover;
        private WorldStateMachine _worldStateMachine;

        [Inject]
        private void Construct(IUIFactory uiFactory, ICurrencyMapData mapData, ICurrencyGameplayMover gameplayMover, WorldStateMachine worldStateMachine)
        {
            _uiFactory = uiFactory;
            this._mapData = mapData;
            _gameplayMover = gameplayMover;
            _worldStateMachine = worldStateMachine;

            _storeItems = new List<BuildingStoreItem>();

            _mapData.WorldStore.BuildingsStoreListUpdated += OnBuildingsStoreListUpdated;
            _mapData.WorldStore.Cleared += OnStoreCleared;

            foreach (BuildingStoreItemData data in this._mapData.WorldStore.BuildingsStoreList)
            {
                CreateStoreItem(data.Type);
            }
        }

        private void OnDestroy()
        {
            _mapData.WorldStore.BuildingsStoreListUpdated -= OnBuildingsStoreListUpdated;
            _mapData.WorldStore.Cleared -= OnStoreCleared;

            foreach (BuildingStoreItem storeItem in _storeItems)
            {
                storeItem.Buyed -= OnStoreItemBuyed;
            }
        }

        private void OnStoreItemBuyed(BuildingType buildingType, uint price)
        {
            if (_mapData.WorldWallet.TryGet(price))
            {
                _mapData.WorldStore.GetBuildingData(buildingType).ChangeBuyingCount();
                _gameplayMover.ChangeBuildingForPlacing(buildingType, price);
                _worldStateMachine.Enter<WorldChangingState>();
            }
        }

        private void CreateStoreItem(BuildingType buildingType)
        {
            BuildingStoreItem storeItem = _uiFactory.CreateStoreItem(buildingType, transform);

            storeItem.Buyed += OnStoreItemBuyed;
            _storeItems.Add(storeItem);
        }

        private void OnBuildingsStoreListUpdated(BuildingType type)
        {
            CreateStoreItem(type);
        }

        private void OnStoreCleared()
        {
            foreach (BuildingStoreItem storeItem in _storeItems)
            {
                storeItem.Buyed -= OnStoreItemBuyed;
                Destroy(storeItem.gameObject);
            }

            _storeItems.Clear();

            foreach (BuildingStoreItemData data in _mapData.WorldStore.BuildingsStoreList)
            {
                CreateStoreItem(data.Type);
            }
        }
    }
}
