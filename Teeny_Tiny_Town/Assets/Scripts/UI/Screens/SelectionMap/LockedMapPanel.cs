using Assets.Scripts.GameLogic.Map;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.SaveLoadServices;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens.SelectionMap
{
    public class LockedMapPanel : MapSelectionPanel
    {
        [SerializeField] private Button _buyButton;
        [SerializeField] private TMP_Text _costValue;
        [SerializeField] private MapSelectionScreen mapSelectionScreen;

        private IStaticDataService _staticDataServcie;
        private IPersistantProgrss _persistentProgress;
        private ISaveLoadService _saveLoadService;
        private MapSwitcher _mapSwitcher;

        private uint _currentWorldCost;

        [Inject]
        private void Construct(
            IPersistantProgrss persistentProgress,
            IStaticDataService staticDataService,
            MapSwitcher mapSwitcher,
            ISaveLoadService saveLoadService)
        {
            _persistentProgress = persistentProgress;
            _staticDataServcie = staticDataService;
            _mapSwitcher = mapSwitcher;
            _saveLoadService = saveLoadService;

            _buyButton.onClick.AddListener(OnBuyButtonClicked);
        }

        private void OnDestroy()
        {
            _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
        }

        public override void Open()
        {
            base.Open();

            MapConfig worldConfig = _staticDataServcie.GetMap<MapConfig>(_mapSwitcher.CurrentWorldDataId);
            _currentWorldCost = worldConfig.Cost;
            _costValue.text = _currentWorldCost.ToString();
        }

        private void OnBuyButtonClicked()
        {
            if (_persistentProgress.Progress.Wallet.TryGet(_currentWorldCost))
            {
                _persistentProgress.Progress.GetMapData(_mapSwitcher.CurrentWorldDataId).IsUnlocked = true;
                mapSelectionScreen.ChangeCurrentWorldInfo(_mapSwitcher.CurrentWorldDataId);
                _saveLoadService.SaveProgress();
            }
        }
    }
}
