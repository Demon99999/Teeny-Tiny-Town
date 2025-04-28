using System;
using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.GameLogic.Mover.Comand;
using Assets.Scripts.Services.Input;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Mover
{
    public class GameplayMover : IGameplayMover, IDisposable
    {
        protected readonly IWorldChanger WorldChanger;
        protected readonly IMapData MapData;
        protected readonly NextBuildingForPlacingCreator NextBuildingForPlacingCreator;

        private readonly IInputService _inputService;
        protected IPersistantProgrss _persistentProgressService;

        private bool _isUndoStarted;

        public GameplayMover(
            IWorldChanger worldChanger,
            IInputService inputService,
            IMapData mapData,
            IPersistantProgrss persistentProgressService,
            NextBuildingForPlacingCreator nextBuildingForPlacingCreator)
        {
            WorldChanger = worldChanger;
            _inputService = inputService;
            MapData = mapData;
            _persistentProgressService = persistentProgressService;
            NextBuildingForPlacingCreator = nextBuildingForPlacingCreator;

            _isUndoStarted = false;

            _inputService.UndoButtonPressed += TryUndoCommand;
        }

        protected Command LastCommand { get; private set; }

        public void Dispose()
        {
            _inputService.UndoButtonPressed -= TryUndoCommand;
        }

        public virtual void PlaceNewBuilding(Vector2Int gridPosition, BuildingType type)
        {
            ExecuteCommand(new PlaceNewBuildingCommand(WorldChanger, gridPosition, MapData, type, NextBuildingForPlacingCreator, _persistentProgressService));
        }

        public void RemoveBuilding(Vector2Int gridPosition)
        {
            ExecuteCommand(new RemoveBuildingCommand(WorldChanger, MapData, gridPosition, NextBuildingForPlacingCreator, _persistentProgressService));
        }

        public virtual void OpenChest(Vector2Int chestGridPosition, uint reward)
        {
            ExecuteCommand(new RemoveChestCommand(WorldChanger, MapData, chestGridPosition, NextBuildingForPlacingCreator, _persistentProgressService));
        }

        public void ReplaceBuilding(Vector2Int fromGridPosition, BuildingType fromBuildingType, Vector2Int toGridPosition, BuildingType toBuildingType)
        {
            ExecuteCommand(new ReplaceBuildingCommand(
                WorldChanger,
                MapData,
                fromGridPosition,
                fromBuildingType,
                toGridPosition,
                toBuildingType,
                NextBuildingForPlacingCreator,
                _persistentProgressService));
        }

        public void TryUndoCommand()
        {
            if (LastCommand == null || _isUndoStarted)
            {
                return;
            }

            _isUndoStarted = true;

            LastCommand.Undo();
            LastCommand = null;

            _isUndoStarted = false;
        }

        protected virtual void ExecuteCommand(Command command)
        {
            LastCommand = command;
            command.Execute();
        }
    }
}
