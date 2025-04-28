using System;
using Assets.Scripts.GameLogic.Map.StateMachineMap;
using Assets.Scripts.GameLogic.StateMashine;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map
{
    public class WaitingWindow : BluredBackgroundWindow
    {
        private const int RewardID = 3;

        [SerializeField] private Button _mapSelectionWindowOpenButton;
        [SerializeField] private Button _startWindowOpenButton;
        [SerializeField] private Button _adButton;

        private WorldStateMachine _worldStateMachine;
        private GameplayStateMachine _gameplayStateMachine;
        private IStaticDataService _staticDataService;
        private IPersistantProgrss _persistentProgressService;

        [Inject]
        private void Construct(
            WorldStateMachine worldStateMachine,
            GameplayStateMachine gameplayStateMachine,
            IStaticDataService staticDataService,
            IPersistantProgrss persistentProgressService)
        {
            _worldStateMachine = worldStateMachine;
            _gameplayStateMachine = gameplayStateMachine;
            _staticDataService = staticDataService;
            _persistentProgressService = persistentProgressService;

            _mapSelectionWindowOpenButton.onClick.AddListener(OnMapSelectionWindowOpenButtonClicked);
            _startWindowOpenButton.onClick.AddListener(OnStartWindowOpenButtonClicked);
            _adButton.onClick.AddListener(OnAdButtonClicked);
            YandexGame.RewardVideoEvent += OnRewarded;
        }

        private void OnDestroy()
        {
            _mapSelectionWindowOpenButton.onClick.RemoveListener(OnMapSelectionWindowOpenButtonClicked);
            _startWindowOpenButton.onClick.RemoveListener(OnStartWindowOpenButtonClicked);
            _adButton.onClick.RemoveListener(OnAdButtonClicked);
            YandexGame.RewardVideoEvent -= OnRewarded;
        }

        private void OnAdButtonClicked()
        {
            YandexGame.RewVideoShow(RewardID);
        }

        private void OnRewarded(int id)
        {
            if (id == RewardID)
            {
                _persistentProgressService.Progress.GameplayMovesCounter.SetCount(_staticDataService.MapsConfig.AvailableMovesCount);
                _worldStateMachine.Enter<WorldStartState>();
            }
        }

        private void OnStartWindowOpenButtonClicked()
        {
            _worldStateMachine.Enter<ExitWorldState, Action>(() => _gameplayStateMachine.Enter<GameStartState>());
        }

        private void OnMapSelectionWindowOpenButtonClicked()
        {
            _worldStateMachine.Enter<ExitWorldState, Action>(() => _gameplayStateMachine.Enter<MapSelectionState>());
        }
    }
}
