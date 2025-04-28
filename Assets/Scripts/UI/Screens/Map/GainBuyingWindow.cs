using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.StateMachineMap;
using Assets.Scripts.Infrastructure.AssetPro;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs;
using Assets.Scripts.Services.StaticDataServices.Configs.Store;
using Assets.Scripts.UI.Screens.Map.Panels.Store;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map
{
    public class GainBuyingWindow : BluredBackgroundWindow
    {
        private const uint MinPurchasedQuantity = 1;
        private const uint MaxPurchasedQuantity = 10;

        [SerializeField] private Image _gainIcon;
        [SerializeField] private Button _increaseQuantityButton;
        [SerializeField] private Button _decreaseQuantityButton;
        [SerializeField] private TMP_Text _costValue;
        [SerializeField] private TMP_Text _countValue;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _hideButton;

        private UnlimitedQuantityGainBuyer _gainBuyer;
        private IStaticDataService _staticDataService;
        private IAssetProvider _assetProvider;
        private AnimationsConfig _animationConfig;
        private ICurrencyMapData _currencyMapData;
        private WorldStateMachine _worldStateMachine;

        private uint _purchasedQuantity;
        private GainStoreItemConfig _gainStoreItemConfig;
        private uint _cost;

        [Inject]
        private void Construct(
            UnlimitedQuantityGainBuyer gainBuyer,
            IStaticDataService staticDataService,
            IAssetProvider assetProvider,
            ICurrencyMapData currencyMapData,
            WorldStateMachine worldStateMachine)
        {
            _gainBuyer = gainBuyer;
            _staticDataService = staticDataService;
            _assetProvider = assetProvider;
            _animationConfig = _staticDataService.AnimationsConfig;
            _currencyMapData = currencyMapData;
            _worldStateMachine = worldStateMachine;

            _increaseQuantityButton.onClick.AddListener(OnIncreaseQuantityButtonClicked);
            _decreaseQuantityButton.onClick.AddListener(OnDecreaseQuantityButtonClicked);
            _buyButton.onClick.AddListener(OnBuyButtonClicked);
            _hideButton.onClick.AddListener(EnterWorldStatrState);
        }

        private void OnDestroy()
        {
            _increaseQuantityButton.onClick.RemoveListener(OnIncreaseQuantityButtonClicked);
            _decreaseQuantityButton.onClick.RemoveListener(OnDecreaseQuantityButtonClicked);
            _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
            _hideButton.onClick.RemoveListener(EnterWorldStatrState);
        }

        public override void Open()
        {
            _gainIcon.sprite = _staticDataService.GetGainStoreItem(_gainBuyer.GainStoreItemType).SpriteconAssetReference;
            _purchasedQuantity = MinPurchasedQuantity;
            _gainStoreItemConfig = _staticDataService.GetGainStoreItem(_gainBuyer.GainStoreItemType);

            ChangeView();

            base.Open();
        }

        private void OnDecreaseQuantityButtonClicked()
        {
            _purchasedQuantity--;
            ChangeView();
        }

        private void OnIncreaseQuantityButtonClicked()
        {
            _purchasedQuantity++;
            ChangeView();
        }

        private void ChangeView()
        {
            _purchasedQuantity = (uint)Mathf.Clamp(_purchasedQuantity, MinPurchasedQuantity, MaxPurchasedQuantity);

            uint buyingCount = _currencyMapData.WorldStore.GetGainData(_gainBuyer.GainStoreItemType).BuyingCount;

            _cost = _gainStoreItemConfig.GetCostsSum(buyingCount, buyingCount + _purchasedQuantity);

            _countValue.text = _purchasedQuantity.ToString();
            _costValue.text = _cost.ToString();
            _costValue.color = _currencyMapData.WorldWallet.Value >= _cost ? _animationConfig.PurchasePermittingColor : _animationConfig.ProhibitingPurchaseColor;
        }

        private void OnBuyButtonClicked()
        {
            if (_currencyMapData.WorldWallet.TryGet(_cost))
            {
                _gainBuyer.ChangeGainItemsCoutn(_purchasedQuantity);
                EnterWorldStatrState();
            }
        }

        private void EnterWorldStatrState()
        {
            _worldStateMachine.Enter<WorldStartState>();
        }
    }
}