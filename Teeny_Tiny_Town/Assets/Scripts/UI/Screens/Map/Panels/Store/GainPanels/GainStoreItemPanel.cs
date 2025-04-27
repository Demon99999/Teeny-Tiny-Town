using System;
using Assets.Scripts.Data.Map;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs;
using Assets.Scripts.Services.StaticDataServices.Configs.Store;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map.Panels.Store.GainPanels
{
    public abstract class GainStoreItemPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _costValue;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Button _buyButton;

        private IStaticDataService _staticDataService;
        private AnimationsConfig _animationsConfig;

        protected ICurrencyMapData CurrencyMapData { get; private set; }
        protected GainStoreItemType Type { get; private set; }
        protected abstract GainStoreItemData Data { get; }
        protected GainBuyer GainBuyer { get; private set; }
        protected uint Cost => _staticDataService.GetGainStoreItem(Type).GetCost(Data.BuyingCount + 1);
        protected Button BuyButton => _buyButton;

        [Inject]
        private void Construct(ICurrencyMapData currencyMapData, IStaticDataService staticDataService, GainBuyer gainBuyer)
        {
            CurrencyMapData = currencyMapData;
            _staticDataService = staticDataService;
            GainBuyer = gainBuyer;
            _animationsConfig = _staticDataService.AnimationsConfig;

            CurrencyMapData.WorldWallet.ValueChanged += ChangeCostValue;
            _buyButton.onClick.AddListener(OnBuyButtonClicked);
        }

        private void OnDestroy()
        {
            CurrencyMapData.WorldWallet.ValueChanged -= ChangeCostValue;
            _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
            Data.BuyingCountChanged -= OnBuyingCountChanged;
        }

        public virtual void Init(GainStoreItemType type, Sprite icon)
        {
            Type = type;

            GetData();

            _icon.sprite = icon;
            _name.text = _staticDataService.GetGainStoreItem(type).Name;

            ChangeCostValue(CurrencyMapData.WorldWallet.Value);

            Data.BuyingCountChanged += OnBuyingCountChanged;
        }

        protected abstract void GetData();

        protected virtual void ChangeCostValue(uint worldWalletValue)
        {
            _costValue.text = DigitUtils.CutDigit(Cost);
            _costValue.color = worldWalletValue >= Cost ? _animationsConfig.PurchasePermittingColor : _animationsConfig.ProhibitingPurchaseColor;
        }

        protected abstract void OnBuyButtonClicked();

        private void OnBuyingCountChanged()
        {
            ChangeCostValue(CurrencyMapData.WorldWallet.Value);
        }
    }
}
