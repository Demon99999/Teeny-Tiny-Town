using Assets.Scripts.GameLogic.Map.StateMachineMap;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Screens.Map
{
    public class CurrencyGameplayWindow : GameplayWindow
    {
        [SerializeField] private Button _storeButton;

        protected override void Subscrube()
        {
            base.Subscrube();
            _storeButton.onClick.AddListener(OnStoreButtonClicked);
        }

        protected override void Unsubscruby()
        {
            base.Unsubscruby();
            _storeButton.onClick.RemoveListener(OnStoreButtonClicked);
        }

        private void OnStoreButtonClicked()
        {
            WorldStateMachine.Enter<StoreState>();
        }
    }
}
