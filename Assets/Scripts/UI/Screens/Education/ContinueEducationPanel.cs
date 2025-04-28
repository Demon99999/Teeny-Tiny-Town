using Assets.Scripts.GameLogic.Map.Representation.ActionHandler;
using Assets.Scripts.GameLogic.Map.Representation.Markers;
using Assets.Scripts.Services.Input;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens.Education
{
    public class ContinueEducationPanel : EducationPanel
    {
        [SerializeField] private Button _continueButton;

        public IInputService InputService { get; private set; }
        public ActionHandlerStateMachine ActionHandlerStateMachine { get; private set; }
        public MarkersVisibility MarkersVisibility { get; private set; }

        [Inject]
        private void Construct(IInputService inputService, ActionHandlerStateMachine actionHandlerStateMachine, MarkersVisibility markersVisibility)
        {
            InputService = inputService;
            ActionHandlerStateMachine = actionHandlerStateMachine;
            MarkersVisibility = markersVisibility;

            _continueButton.onClick.AddListener(OnHandled);
        }

        private void OnDestroy()
        {
            _continueButton.onClick.RemoveListener(OnHandled);
        }

        public override void Open()
        {
            base.Open();
            MarkersVisibility.ChangeAllowedVisibility(false);
            InputService.SetEnabled(false);
            ActionHandlerStateMachine.SetActive(false);
        }
    }
}
