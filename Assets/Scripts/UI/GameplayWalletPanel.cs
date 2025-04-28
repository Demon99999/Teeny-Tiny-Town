using Assets.Scripts.Services.PersistantProgrssService;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
    public class GameplayWalletPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _value;

        private IPersistantProgrss _persistentProgressService;

        [Inject]
        private void Construct(IPersistantProgrss persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;

            ChangeValue(_persistentProgressService.Progress.Wallet.Value);

            _persistentProgressService.Progress.Wallet.ValueChanged += ChangeValue;
        }

        private void OnDestroy()
        {
            _persistentProgressService.Progress.Wallet.ValueChanged -= ChangeValue;
        }

        private void ChangeValue(uint value)
        {
            _value.text = value.ToString();
        }
    }
}
