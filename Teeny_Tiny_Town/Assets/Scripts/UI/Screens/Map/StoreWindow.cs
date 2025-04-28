using Assets.Scripts.GameLogic.Map.StateMachineMap;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map
{
    public class StoreWindow : BluredBackgroundWindow
    {
        [SerializeField] private Button _hideButton;

        private WorldStateMachine _worldStateMachine;

        [Inject]
        private void Construct(WorldStateMachine worldStateMachine)
        {
            _worldStateMachine = worldStateMachine;
        }

        private void OnEnable()
        {
            _hideButton.onClick.AddListener(OnHideButtonClicked);
        }

        private void OnDisable()
        {
            _hideButton.onClick.RemoveListener(OnHideButtonClicked);
        }

        private void OnHideButtonClicked()
        {
            _worldStateMachine.Enter<WorldChangingState>();
        }
    }
}
