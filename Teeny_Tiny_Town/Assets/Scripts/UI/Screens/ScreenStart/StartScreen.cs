using Assets.Scripts.GameLogic.StateMashine;
using Assets.Scripts.Infrastructure.State;
using Assets.Scripts.Infrastructure.StateMachine.State;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens.ScreenStart
{
    public class StartScreen : ScreenBase
    {
        [SerializeField] private Button _mapSelectionButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _sandboxButton;
        [SerializeField] private Button _collectionButton;
        [SerializeField] private Button _questsButton;

        private GameStateMachine _gameStateMachine;
        private GameplayStateMachine _gameplayStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine, GameplayStateMachine gameplayStateMachine)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _gameStateMachine = gameStateMachine;

            _mapSelectionButton.onClick.AddListener(OnMapSelectionButtonClicked);
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
            _sandboxButton.onClick.AddListener(OnSandboxButtonClicked);
            _collectionButton.onClick.AddListener(OnCollectionButtonClicked);
            _questsButton.onClick.AddListener(OnQuestsButtonClicked);
        }

        private void OnDestroy()
        {
            _mapSelectionButton.onClick.RemoveListener(OnMapSelectionButtonClicked);
            _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
            _sandboxButton.onClick.AddListener(OnSandboxButtonClicked);
            _collectionButton.onClick.RemoveListener(OnCollectionButtonClicked);
            _questsButton.onClick.RemoveListener(OnQuestsButtonClicked);
        }

        private void OnMapSelectionButtonClicked()
        {
            _gameplayStateMachine.Enter<MapSelectionState>();
        }

        private void OnContinueButtonClicked()
        {
            _gameplayStateMachine.Enter<GameplayLoopState, bool>(false);
        }

        private void OnSandboxButtonClicked()
        {
            _gameStateMachine.Enter<SandboxState>();
        }

        private void OnCollectionButtonClicked()
        {
            _gameStateMachine.Enter<CollectionState>();
        }

        private void OnQuestsButtonClicked()
        {
            _gameplayStateMachine.Enter<ShowQuestsState>();
        }
    }
}
