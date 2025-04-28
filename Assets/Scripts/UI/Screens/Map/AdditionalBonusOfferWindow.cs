using System;
using Assets.Scripts.GameLogic.Map.StateMachineMap;
using Assets.Scripts.GameLogic.StateMashine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map
{
    public class AdditionalBonusOfferWindow : BluredBackgroundWindow
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _hideWindowButton;

        private WorldStateMachine _worldStateMachine;
        private GameplayStateMachine _gameplayStateMachine;

        [Inject]
        private void Construct(WorldStateMachine worldStateMachine, GameplayStateMachine gameplayStateMachine)
        {
            _worldStateMachine = worldStateMachine;
            _gameplayStateMachine = gameplayStateMachine;

            _startButton.onClick.AddListener(OnStartButtonClicked);
            _hideWindowButton.onClick.AddListener(OnHideWindowButtonClicked);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClicked);
            _hideWindowButton.onClick.RemoveListener(OnHideWindowButtonClicked);
        }

        private void OnStartButtonClicked()
        {
            _worldStateMachine.Enter<WorldChangingState>();
        }

        private void OnHideWindowButtonClicked()
        {
            _worldStateMachine.Enter<ExitWorldState, Action>(() => _gameplayStateMachine.Enter<MapSelectionState>());
        }
    }
}
