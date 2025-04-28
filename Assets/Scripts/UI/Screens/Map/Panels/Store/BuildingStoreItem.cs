using System;
using Assets.Scripts.Data;
using Assets.Scripts.Data.Map;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map.Panels.Store
{
    public class BuildingStoreItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _costValue;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _buyButton;

        private ICurrencyMapData _currencyMapData;
        private IStaticDataService _staticDataService;
        private AnimationsConfig _animationsConfig;

        private BuildingType _buildingType;
        private BuildingStoreItemData _data;

        public event Action<BuildingType, uint> Buyed;

        private uint Cost => _staticDataService.GetBuildingStoreItem(_buildingType).GetCost(_data.BuyingCount + 1);

        [Inject]
        private void Construct(ICurrencyMapData currencyMapData, IStaticDataService staticDataService)
        {
            _currencyMapData = currencyMapData;

            _staticDataService = staticDataService;
            _animationsConfig = staticDataService.AnimationsConfig;

            _buyButton.onClick.AddListener(OnBuyButtonClicked);
            _currencyMapData.WorldWallet.ValueChanged += ChangeCostValue;
        }

        private void OnDestroy()
        {
            _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
            _data.BuyingCountChanged -= OnBuyingCountChanged;
        }

        public void Init(BuildingType buildingType, Sprite icon)
        {
            _buildingType = buildingType;
            _data = _currencyMapData.WorldStore.GetBuildingData(_buildingType);

            ChangeCostValue(_currencyMapData.WorldWallet.Value);

            _data.BuyingCountChanged += OnBuyingCountChanged;

            _icon.sprite = icon;
            _icon.SetNativeSize();
        }

        private void OnBuyingCountChanged()
        {
            ChangeCostValue(_currencyMapData.WorldWallet.Value);
        }

        private void OnBuyButtonClicked()
        {
            Buyed?.Invoke(_buildingType, Cost);
        }

        private void ChangeCostValue(uint worldWalletValue)
        {
            _costValue.text = DigitUtils.CutDigit(Cost);
            _costValue.color = worldWalletValue >= Cost ? _animationsConfig.PurchasePermittingColor : _animationsConfig.ProhibitingPurchaseColor;
        }
    }
}
