using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map;
using Assets.Scripts.GameLogic.StateMashine;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens.SelectionMap
{
    public class MapSelectionScreen : ScreenBase
    {
        [SerializeField] private Button _nextMapButton;
        [SerializeField] private Button _previousMapButton;
        [SerializeField] private Button _hideButton;
        [SerializeField] private MapSelectionPanel _startPanel;
        [SerializeField] private MapSelectionPanel _lcockedMapPanel;
        [SerializeField] private MapSelectionPanel _continuePanel;
        [SerializeField] private TMP_Text _sizeValue;
        [SerializeField] private TMP_Text _name;

        private MapSwitcher _mapSwitcher;
        private GameplayStateMachine _gameplayStateMachine;
        private IPersistantProgrss _persistentProgressService;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(
            MapSwitcher mapSwitcher,
            GameplayStateMachine gameplayStateMachine,
            IPersistantProgrss persistentProgressService,
            IStaticDataService staticDataService)
        {
            _mapSwitcher = mapSwitcher;
            _gameplayStateMachine = gameplayStateMachine;
            _persistentProgressService = persistentProgressService;
            _staticDataService = staticDataService;

            _nextMapButton.onClick.AddListener(OnNextMapButtonClicked);
            _previousMapButton.onClick.AddListener(OnPreviousMapButtonClicked);
            _hideButton.onClick.AddListener(OnHideButtonClicked);
            _mapSwitcher.CurrentWorldChanged += ChangeCurrentWorldInfo;
        }

        private void OnDestroy()
        {
            _nextMapButton.onClick.RemoveListener(OnNextMapButtonClicked);
            _previousMapButton.onClick.RemoveListener(OnPreviousMapButtonClicked);
            _hideButton.onClick.RemoveListener(OnHideButtonClicked);
            _mapSwitcher.CurrentWorldChanged -= ChangeCurrentWorldInfo;
        }

        public override void Open()
        {
            base.Open();
            ChangeCurrentWorldInfo(_mapSwitcher.CurrentWorldDataId);
        }

        public void ChangeCurrentWorldInfo(string worldDataId)
        {
            MapData mapData = _persistentProgressService.Progress.GetMapData(worldDataId);
            MapConfig worldConfig = _staticDataService.GetMap<MapConfig>(mapData.Id);
            Vector2Int size = mapData.Size;

            _sizeValue.text = $"{size.x}x{size.y}";
            _name.text = worldConfig.Name;

            if (mapData.IsUnlocked)
            {
                if (mapData.IsChangingStarted)
                {
                    _startPanel.Hide();
                    _continuePanel.Open();
                }
                else
                {
                    _startPanel.Open();
                    _continuePanel.Hide();
                }

                _lcockedMapPanel.Hide();
            }
            else
            {
                _startPanel.Hide();
                _continuePanel.Hide();
                _lcockedMapPanel.Open();
            }
        }

        private void OnPreviousMapButtonClicked()
        {
            _mapSwitcher.ShowPreviousWorld();
        }

        private void OnNextMapButtonClicked()
        { 
            _mapSwitcher.ShowNextWorld();
        }

        private void OnHideButtonClicked()
        {
            _gameplayStateMachine.Enter<GameStartState>();
        }
    }
}