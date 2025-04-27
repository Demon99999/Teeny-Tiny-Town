using System;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.GameLogic.Map.StateMachineMap;
using Assets.Scripts.GameLogic.StateMashine;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Services.PersistantProgrssService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map
{
    public class GameplayWindow : ScreenBase
    {
        [SerializeField] private Button _hideButton;
        [SerializeField] private Button _questsWindowOpenButton;
        [SerializeField] private Transform _remainingMovesPanelParent;
        [SerializeField] private Transform _rotationPanelParent;

        private NextBuildingForPlacingCreator _nextBuildingForPlacingCreator;
        private GameplayStateMachine _gameplayStateMachine;

        protected WorldStateMachine WorldStateMachine { get; private set; }

        [Inject]
        private void Construct(
            WorldStateMachine worldStateMachine,
            NextBuildingForPlacingCreator nextBuildingForPlacingCreator,
            GameplayStateMachine gameplayStateMachine,
            IPersistantProgrss persistentProgressService,
            IUIFactory uiFactory,
            GameLogic.Map.Map map)
        {
            WorldStateMachine = worldStateMachine;
            _nextBuildingForPlacingCreator = nextBuildingForPlacingCreator;
            _gameplayStateMachine = gameplayStateMachine;

            if (persistentProgressService.Progress.StoreData.IsInfinityMovesUnlocked == false)
            {
                uiFactory.CreateRemainingMovesPanel(_remainingMovesPanelParent);
            }

            if (persistentProgressService.Progress.SettingsData.IsRotationSnapped == false)
            {
                uiFactory.CreateRotationPanel(_rotationPanelParent);
            }

            Subscrube();
        }

        private void OnDestroy()
        {
            Unsubscruby();
        }

        protected virtual void Subscrube()
        {
            _hideButton.onClick.AddListener(OnHideButtonClicked);
            _nextBuildingForPlacingCreator.NoMoreEmptyTiles += OnNoMoreEmptyTiles;
            _questsWindowOpenButton.onClick.AddListener(OnQuestsWindowOpenButtonClicked);
        }

        protected virtual void Unsubscruby()
        {
            _hideButton.onClick.RemoveListener(OnHideButtonClicked);
            _nextBuildingForPlacingCreator.NoMoreEmptyTiles -= OnNoMoreEmptyTiles;
            _questsWindowOpenButton.onClick.RemoveListener(OnQuestsWindowOpenButtonClicked);
        }

        private void OnQuestsWindowOpenButtonClicked()
        {
            WorldStateMachine.Enter<QuestsState>();
        }

        private void OnHideButtonClicked()
        {
            WorldStateMachine.Enter<ExitWorldState, Action>(() => _gameplayStateMachine.Enter<MapSelectionState>());
        }

        private void OnNoMoreEmptyTiles()
        {
            WorldStateMachine.Enter<SafeGameplayState>();
        }
    }
}