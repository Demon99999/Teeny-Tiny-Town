using Assets.Scripts.GameLogic.Mover;
using Assets.Scripts.Services.StaticDataServices.Configs.Store;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map.Panels.Store.GainPanels
{
    public class UnlimitedQuantityGainStoreItemPanel : GainStoreItemPanel
    {
        private GainStoreItemData _data;
        private ICurrencyGameplayMover _gameplayMover;

        protected override GainStoreItemData Data => _data;

        protected override void GetData()
        {
            _data = CurrencyMapData.WorldStore.GetGainData(Type);
        }

        [Inject]
        private void Construct(ICurrencyGameplayMover gameplayMover)
        {
            _gameplayMover = gameplayMover;
        }

        protected override void OnBuyButtonClicked()
        {
            if (CurrencyMapData.WorldWallet.Value >= Cost)
            {
                _gameplayMover.BuyGainStoreItem(Type, Cost);
            }
        }
    }
}