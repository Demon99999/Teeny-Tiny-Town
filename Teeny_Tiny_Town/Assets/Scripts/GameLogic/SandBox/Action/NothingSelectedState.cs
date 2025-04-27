using Assets.Scripts.GameLogic.Map.Representation.ActionHandler;
using Assets.Scripts.GameLogic.Map.Representation.Markers;
using UnityEngine;

namespace Assets.Scripts.GameLogic.SandBox.Action
{
    public class NothingSelectedState : ActionHandlerState
    {
        private readonly ActionHandlerStateMachine _actionHandlerStateMachine;

        public NothingSelectedState(SelectFrame selectFrame, LayerMask layerMask, ActionHandlerStateMachine actionHandlerStateMachine)
            : base(selectFrame, layerMask)
        {
            _actionHandlerStateMachine = actionHandlerStateMachine;
        }

        public override void Enter()
        {
            _actionHandlerStateMachine.SetActive(false);
        }

        public override void Exit()
        {
            _actionHandlerStateMachine.SetActive(true);
        }

        public override void OnHandleMoved(Vector2 handlePosition) { }
        
        public override void OnPressed(Vector2 handlePosition) { }
    }
}
