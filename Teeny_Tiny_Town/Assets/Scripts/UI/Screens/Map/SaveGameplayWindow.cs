using Assets.Scripts.GameLogic.Map.StateMachineMap;
using Assets.Scripts.GameLogic.Mover;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map
{
    public class SaveGameplayWindow : BluredBackgroundWindow
    {
        [SerializeField] private Button _undoButton;
        [SerializeField] private Button _completeButton;

        private IGameplayMover _gameplayMover;
        private WorldStateMachine _worldStateMachine;

        [Inject]
        private void Construct(IGameplayMover gameplayMover, WorldStateMachine worldStateMachine)
        {
            _gameplayMover = gameplayMover;
            _worldStateMachine = worldStateMachine;

            _undoButton.onClick.AddListener(OnUndoButtonClicked);
            _completeButton.onClick.AddListener(OnCompleteButtonClicked);
        }

        private void OnDestroy()
        {
            _undoButton.onClick.RemoveListener(OnUndoButtonClicked);
            _completeButton.onClick.RemoveListener(OnCompleteButtonClicked);
        }

        private void OnCompleteButtonClicked()
        {
            _worldStateMachine.Enter<ResultState>();
        }

        private void OnUndoButtonClicked()
        {
            _gameplayMover.TryUndoCommand();
            _worldStateMachine.Enter<WorldStartState>();
        }
    }
}
