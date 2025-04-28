using Assets.Scripts.GameLogic.Map;
using Assets.Scripts.GameLogic.StateMashine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens.SelectionMap
{
    public class ContinueMapSelectionPanel : MapSelectionPanel
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;

        private GameplayStateMachine _gameplayStateMachine;
        private MapSwitcher _mapSwitcher;

        [Inject]
        private void Construct(GameplayStateMachine gameplayStateMachine, MapSwitcher mapSwitcher)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _mapSwitcher = mapSwitcher;

            _continueButton.onClick.AddListener(OnContinueButtonClicked);
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
        }

        private void OnDestroy()
        {
            _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        }

        private void OnRestartButtonClicked()
        {
            _mapSwitcher.CleanCurrentWorld();
            _mapSwitcher.StartLastWorld();
        }

        private void OnContinueButtonClicked()
        {
            _gameplayStateMachine.Enter<GameplayLoopState, bool>(true);
        }
    }
}
