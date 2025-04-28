using Assets.Scripts.Infrastructure.State;
using Assets.Scripts.Infrastructure.StateMachine.State;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens.SandBox
{
    public class SandboxWindow : ScreenBase
    {
        [SerializeField] private Button _hideButton;

        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;

            _hideButton.onClick.AddListener(OnHideButtonClicked);
        }

        private void OnDestroy()
        {
            _hideButton.onClick.RemoveListener(OnHideButtonClicked);
        }

        private void OnHideButtonClicked()
        {
            _gameStateMachine.Enter<GameLoopState>();
        }
    }
}
