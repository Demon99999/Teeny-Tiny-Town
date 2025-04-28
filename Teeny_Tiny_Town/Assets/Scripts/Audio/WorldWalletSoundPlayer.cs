using Assets.Scripts.Data.Map;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Audio
{
    public class WorldWalletSoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private ICurrencyMapData _currencyMapData;

        [Inject]
        private void Construct(ICurrencyMapData currencyMapData)
        {
            _currencyMapData = currencyMapData;
            _currencyMapData.WorldWallet.ValueChanged += OnWalletValueChanged;
        }

        private void OnDestroy()
        {
            _currencyMapData.WorldWallet.ValueChanged -= OnWalletValueChanged;
        }

        private void OnWalletValueChanged(uint obj)
        {
            _audioSource.Play();
        }

        public class Factory : PlaceholderFactory<WorldWalletSoundPlayer>
        {

        }
    }
}
