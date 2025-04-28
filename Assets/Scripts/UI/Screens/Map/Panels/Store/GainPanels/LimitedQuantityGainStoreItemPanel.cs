using Assets.Scripts.GameLogic.Map.StateMachineMap;
using Assets.Scripts.GameLogic.Mover;
using Assets.Scripts.Services.StaticDataServices.Configs.Store;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map.Panels.Store.GainPanels
{
    public class LimitedQuantityGainStoreItemPanel : GainStoreItemPanel
    {
        [SerializeField] private TMP_Text _remainCountValue;
        [SerializeField] private CanvasGroup _buyButtonCanvasGroup;
        [SerializeField] private CanvasGroup _endedItemsInfogCanvasGroup;

        private WorldStateMachine _worldStateMachine;
        private ICurrencyGameplayMover _gameplayMover;

        private GainStoreItemData _data;

        protected override GainStoreItemData Data => _data;

        [Inject]
        private void Construct(WorldStateMachine worldStateMachine, ICurrencyGameplayMover gameplayMover)
        {
            _worldStateMachine = worldStateMachine;
            _gameplayMover = gameplayMover;
        }

        public override void Init(GainStoreItemType type, Sprite icon)
        {
            base.Init(type, icon);

            _remainCountValue.text = _data.RemainingCount.ToString();
        }

        protected override void GetData()
        {
            _data = CurrencyMapData.WorldStore.GetGainData(Type);
        }

        protected override void OnBuyButtonClicked()
        {
            if (CurrencyMapData.WorldWallet.Value >= Cost && _data.RemainingCount > 0)
            {
                _gameplayMover.BuyGainStoreItem(Type, Cost);
                _worldStateMachine.Enter<WorldStartState>();
            }
        }

        protected override void ChangeCostValue(uint worldWalletValue)
        {
            _remainCountValue.text = _data.RemainingCount.ToString();

            if (_data.RemainingCount != 0)
            {
                base.ChangeCostValue(worldWalletValue);
                SetActiveCanvasGroup(_buyButtonCanvasGroup, true);
                SetActiveCanvasGroup(_endedItemsInfogCanvasGroup, false);
                BuyButton.interactable = true;
            }
            else
            {
                SetActiveCanvasGroup(_buyButtonCanvasGroup, false);
                SetActiveCanvasGroup(_endedItemsInfogCanvasGroup, true);
                BuyButton.interactable = false;
            }
        }

        private void SetActiveCanvasGroup(CanvasGroup canvasGroup, bool isActive)
        {
            canvasGroup.alpha = isActive ? 1 : 0;
            canvasGroup.blocksRaycasts = isActive;
            canvasGroup.interactable = isActive;
        }
    }
}