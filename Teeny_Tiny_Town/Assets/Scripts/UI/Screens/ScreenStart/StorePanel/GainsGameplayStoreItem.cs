using Assets.Scripts.Data.Map;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.SaveLoadServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens.ScreenStart.StorePanel
{
    public class GainsGameplayStoreItem : MonoBehaviour
    {
        private const uint ItemsCount = 2;

        [SerializeField] private uint _cost;
        [SerializeField] private Button _buyButton;
        [SerializeField] private TMP_Text _costValue;

        private IPersistantProgrss _persistentProgressServcie;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IPersistantProgrss persistentProgressService, ISaveLoadService saveLoadService)
        {
            _persistentProgressServcie = persistentProgressService;
            _saveLoadService = saveLoadService;

            _costValue.text = _cost.ToString();

            _buyButton.onClick.AddListener(OnBuyButtonClicked);
        }

        private void OnDestroy()
        {
            _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
        }

        private void OnBuyButtonClicked()
        {
            if (_persistentProgressServcie.Progress.Wallet.TryGet(_cost))
            {
                MapData worldData = _persistentProgressServcie.Progress.GetMapData(_persistentProgressServcie.Progress.LastPlayedWorldDataId);

                worldData.BulldozerItems.AddItems(ItemsCount);
                worldData.ReplaceItems.AddItems(ItemsCount);

                _saveLoadService.SaveProgress();
            }
        }
    }
}
