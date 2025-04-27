using System;
using Assets.Scripts.Data.Map;
using Assets.Scripts.Services.Input;

namespace Assets.Scripts.GameLogic.Map.Representation.ActionHandler
{
    public class ActionHandlerSwitcher : IActionHandlerSwitcher, IDisposable
    {
        private readonly WorldRepresentationChanger _worldRepresentationChanger;
        private readonly ActionHandlerStateMachine _handlerStateMachine;
        private readonly IInputService _inputService;
        private readonly IMapData _mapData;

        public ActionHandlerSwitcher(
            ActionHandlerStateMachine handlerStateMachine,
            WorldRepresentationChanger worldRepresentationChanger,
            IInputService inputService,
            IMapData mapData)
        {
            _handlerStateMachine = handlerStateMachine;
            _worldRepresentationChanger = worldRepresentationChanger;
            _inputService = inputService;
            _mapData = mapData;

            _inputService.RemoveBuildingButtonPressed += OnRemoveBuildingButtonClicked;
            _inputService.ReplaceBuildingButtonPressed += OnReplaceBuildingButtonClicked;
            _worldRepresentationChanger.GameplayMoved += EnterToDefaultState;
        }

        public void Dispose()
        {
            _inputService.RemoveBuildingButtonPressed -= OnRemoveBuildingButtonClicked;
            _inputService.ReplaceBuildingButtonPressed -= OnReplaceBuildingButtonClicked;
            _worldRepresentationChanger.GameplayMoved -= EnterToDefaultState;
        }

        public void EnterToDefaultState()
        {
            if (!(_handlerStateMachine.CurrentState is NewBuildingPlacePositionHandler))
            {
                _handlerStateMachine.Enter<NewBuildingPlacePositionHandler>();
            }
        }

        protected virtual bool CheckBulldozerItemsCount()
        {
            return _mapData.BulldozerItems.Count != 0;
        }

        protected virtual bool CheckReplaceItemsCount()
        {
            return _mapData.ReplaceItems.Count != 0;
        }

        private void OnReplaceBuildingButtonClicked()
        {
            if (CheckReplaceItemsCount() == false)
            {
                return;
            }

            if (_handlerStateMachine.CurrentState is ReplacedBuildingPositionHandler)
            {
                _handlerStateMachine.Enter<NewBuildingPlacePositionHandler>();
            }
            else
                _handlerStateMachine.Enter<ReplacedBuildingPositionHandler>();
        }

        private void OnRemoveBuildingButtonClicked()
        {
            if (CheckBulldozerItemsCount() == false)
                return;

            if (_handlerStateMachine.CurrentState is RemovedBuildingPositionHandler)
            {
                _handlerStateMachine.Enter<NewBuildingPlacePositionHandler>();
            }
            else
            {
                _handlerStateMachine.Enter<RemovedBuildingPositionHandler>();
            }
        }
    }
}
