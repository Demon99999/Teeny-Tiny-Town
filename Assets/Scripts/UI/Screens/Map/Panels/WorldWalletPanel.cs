using Assets.Scripts.Data.Map;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map.Panels
{
    public class WorldWalletPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _walletValue;

        private ICurrencyMapData _mapData;

        [Inject]
        private void Construct(ICurrencyMapData mapData)
        {
            _mapData = mapData;

            _mapData.WorldWallet.ValueChanged += OnWorldWalletValueChanged;

            OnWorldWalletValueChanged(_mapData.WorldWallet.Value);
        }

        private void OnDestroy()
        {
            _mapData.WorldWallet.ValueChanged -= OnWorldWalletValueChanged;
        }

        private void OnWorldWalletValueChanged(uint value)
        {
            _walletValue.text = DigitUtils.CutDigit(value);
        }
    }
}
