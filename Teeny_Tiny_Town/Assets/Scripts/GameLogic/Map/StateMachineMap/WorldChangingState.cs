using System;
using Assets.Scripts.Camera;
using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.GameLogic.Map.Representation.ActionHandler;
using Assets.Scripts.GameLogic.Map.Representation.Markers;
using Assets.Scripts.Infrastructure.StateMachine.State;
using Assets.Scripts.Services.Input;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Screens.Map;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Map.StateMachineMap
{
    public class WorldChangingState : IState, IDisposable
    {
        private readonly IInputService _inputService;
        private readonly ScreensSwitcher _windowsSwitcher;
        private readonly ActionHandlerStateMachine _actionHandlerStateMachine;
        private readonly GameplayCamera _camera;
        private readonly IMapData _worldData;
        private readonly WorldStateMachine _worldStateMachine;
        private readonly IPersistantProgrss _persistentProgressService;
        private readonly MarkersVisibility _markersVisibility;
        private readonly NextBuildingForPlacingCreator _nextBuildingForPlacingCreator;
        private readonly IWorldChanger _worldChanger;

        public WorldChangingState(
            IInputService inputService,
            ScreensSwitcher windowsSwitcher,
            ActionHandlerStateMachine actionHandlerStateMachine,
            GameplayCamera gameplayCamera,
            IMapData worldData,
            WorldStateMachine worldStateMachine,
            IPersistantProgrss persistentProgressService,
            MarkersVisibility markersVisibility,
            IWorldChanger worldChanger,
            NextBuildingForPlacingCreator nextBuildingForPlacingCreator)
        {
            _inputService = inputService;
            _windowsSwitcher = windowsSwitcher;
            _actionHandlerStateMachine = actionHandlerStateMachine;
            _camera = gameplayCamera;
            _worldData = worldData;
            _worldStateMachine = worldStateMachine;
            _persistentProgressService = persistentProgressService;
            _markersVisibility = markersVisibility;
            _worldChanger = worldChanger;
            _nextBuildingForPlacingCreator = nextBuildingForPlacingCreator;

            _worldData.PointsData.GoalAchieved += OnGoalAchived;
        }

        public void Dispose()
        {
            _worldData.PointsData.GoalAchieved -= OnGoalAchived;
            _persistentProgressService.Progress.GameplayMovesCounter.MovesOvered -= OnMovesOvered;
        }

        public void Enter()
        {
            _worldData.IsChangingStarted = true;
            _persistentProgressService.Progress.LastPlayedWorldDataId = _worldData.Id;

            _windowsSwitcher.HideCurrentWindow();

            _camera.MoveTo(new Vector3(55.1f, 78.8f, -55.1f), callback: () =>
            {
                if (_nextBuildingForPlacingCreator.CheckFreeTiles(_worldChanger.Tiles))
                {
                    _actionHandlerStateMachine.SetActive(true);
                    _inputService.SetEnabled(true);
                    _markersVisibility.ChangeAllowedVisibility(true);

                    _windowsSwitcher.Switch<GameplayWindow>();
                }
            });

            _persistentProgressService.Progress.GameplayMovesCounter.MovesOvered += OnMovesOvered;
        }

        public void Exit()
        {
            _actionHandlerStateMachine.SetActive(false);
            _markersVisibility.ChangeAllowedVisibility(false);
            _persistentProgressService.Progress.GameplayMovesCounter.MovesOvered -= OnMovesOvered;
        }

        private void OnMovesOvered()
        {
            _worldStateMachine.Enter<WaitingState>();
        }

        private void OnGoalAchived()
        {
            _worldStateMachine.Enter<RewardState>();
        }
    }
}