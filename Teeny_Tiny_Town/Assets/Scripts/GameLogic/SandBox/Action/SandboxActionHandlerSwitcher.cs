using System;
using Assets.Scripts.GameLogic.Map.Representation.ActionHandler;
using Assets.Scripts.Services.Input;

namespace Assets.Scripts.GameLogic.SandBox.Action
{
    public class SandboxActionHandlerSwitcher : IDisposable
    {
        private readonly ActionHandlerStateMachine _handlerStateMachine;
        private readonly IInputService _inputService;

        public SandboxActionHandlerSwitcher(
            ActionHandlerStateMachine handlerStateMachine,
            IInputService inputService)
        {
            _handlerStateMachine = handlerStateMachine;
            _inputService = inputService;

            _inputService.ClearTilesButtonPressed += OnClearTilesButtonPressed;
            _inputService.BuildingsButtonPressed += OnBuildingsButtonPressed;
            _inputService.GroundsButtonPressed += OnGroundsButtonPressed;
        }

        public void Dispose()
        {
            _inputService.ClearTilesButtonPressed -= OnClearTilesButtonPressed;
            _inputService.BuildingsButtonPressed -= OnBuildingsButtonPressed;
            _inputService.GroundsButtonPressed -= OnGroundsButtonPressed;
        }

        public void EnterToDefaultState()
        {
            if (!(_handlerStateMachine.CurrentState is NothingSelectedState))
            {
                _handlerStateMachine.Enter<NothingSelectedState>();
            }
        }

        private void OnGroundsButtonPressed()
        {
            if (_handlerStateMachine.CurrentState is GroundPositionHandler)
            {
                EnterToDefaultState();
            }
            else
            {
                _handlerStateMachine.Enter<GroundPositionHandler>();
            }
        }

        private void OnBuildingsButtonPressed()
        {
            if (_handlerStateMachine.CurrentState is BuildingPositionHandler)
            {
                EnterToDefaultState();
            }
            else
            {
                _handlerStateMachine.Enter<BuildingPositionHandler>();
            }
        }

        private void OnClearTilesButtonPressed()
        {
            if (_handlerStateMachine.CurrentState is ClearTilePositionHandler)
            {
                EnterToDefaultState();
            }
            else
            {
                _handlerStateMachine.Enter<ClearTilePositionHandler>();
            }
        }
    }
}
