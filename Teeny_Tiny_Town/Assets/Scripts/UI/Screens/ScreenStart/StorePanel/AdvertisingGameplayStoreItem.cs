using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.SaveLoadServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Zenject;

namespace Assets.Scripts.UI.Screens.ScreenStart.StorePanel
{
    public class AdvertisingGameplayStoreItem : MonoBehaviour
    {
        [SerializeField] private int RewardID;
        [SerializeField] private uint _reward;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _rewardValue;

        private IPersistantProgrss _persistentProgressServcie;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IPersistantProgrss persistentProgressService, ISaveLoadService saveLoadService)
        {
            _persistentProgressServcie = persistentProgressService;
            _saveLoadService = saveLoadService;

            _rewardValue.text = _reward.ToString();

            _button.onClick.AddListener(OnButtonClicked);
            YandexGame.RewardVideoEvent += OnRewarded;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
            YandexGame.RewardVideoEvent -= OnRewarded;
        }

        private void OnButtonClicked()
        {
            YandexGame.RewVideoShow(RewardID);
        }

        private void OnRewarded(int id)
        {
            if (id == RewardID)
            {
                _persistentProgressServcie.Progress.Wallet.Give(_reward);
                _saveLoadService.SaveProgress();
            }
        }
    }
}
