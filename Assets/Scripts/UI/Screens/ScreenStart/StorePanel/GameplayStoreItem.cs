using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.SaveLoadServices;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.GameStore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens.ScreenStart.StorePanel
{
    public class GameplayStoreItem : MonoBehaviour
    {
        [SerializeField] private GameplayStoreItemType _type;
        [SerializeField] private Button _buyButton;
        [SerializeField] private TMP_Text _costValue;

        private IPersistantProgrss _persistentProgressService;
        private ISaveLoadService _saveLoadService;

        private StoreItemConfig _storeItemConfig;

        [Inject]
        private void Construct(IPersistantProgrss persistentProgressService, IStaticDataService staticDataService, ISaveLoadService saveLoadService)
        {
            _persistentProgressService = persistentProgressService;
            _saveLoadService = saveLoadService;

            _storeItemConfig = staticDataService.GetGameplayStorItem(_type);

            _costValue.text = _storeItemConfig.Cost.ToString();

            if (_storeItemConfig.NeedToShow(_persistentProgressService.Progress.StoreData) == false)
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            _buyButton.onClick.AddListener(OnBuyButtonClicked);
        }

        private void OnDisable()
        {
            _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
        }

        private void OnBuyButtonClicked()
        {
            if (_persistentProgressService.Progress.Wallet.TryGet(_storeItemConfig.Cost))
            {
                _storeItemConfig.Unlock(_persistentProgressService.Progress.StoreData);
                _saveLoadService.SaveProgress();
                Destroy(gameObject);
            }
        }
    }
}
