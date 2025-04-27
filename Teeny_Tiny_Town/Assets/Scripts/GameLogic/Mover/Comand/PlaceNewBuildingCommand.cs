using System.Collections.Generic;
using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Mover.Comand
{
    public class PlaceNewBuildingCommand : MoveCommand
    {
        private readonly Vector2Int _placedBuildingGridPosition;
        private readonly BuildingType _placedBuildingType;
        private readonly IMapData mapData;

        private readonly BuildingType _nextBuildingTypeForCreation;
        private readonly uint _nextBuildingForCreationBuildsCount;
        private readonly IReadOnlyList<BuildingType> _availableBuildingsForCreation;

        public PlaceNewBuildingCommand(
            IWorldChanger world,
            Vector2Int placedBuildingGridPosition,
            IMapData mapData,
            BuildingType placedBuildingType,
            NextBuildingForPlacingCreator nextBuildingForPlacingCreator,
            IPersistantProgrss persistentProgressService)
            : base(world, mapData, nextBuildingForPlacingCreator, persistentProgressService)
        {
            _placedBuildingGridPosition = placedBuildingGridPosition;
            _placedBuildingType = placedBuildingType;
            this.mapData = mapData;

            _nextBuildingTypeForCreation = this.mapData.NextBuildingTypeForCreation;
            _nextBuildingForCreationBuildsCount = this.mapData.NextBuildingForCreationBuildsCount;
            _availableBuildingsForCreation = this.mapData.AvailableBuildingsForCreation;
        }

        public override void Execute()
        {
            PersistentProgressService.Progress.AddBuildingToCollection(_placedBuildingType);
            WorldChanger.PlaceNewBuilding(_placedBuildingGridPosition, _placedBuildingType);
            base.Execute();
        }

        public override void Undo()
        {
            base.Undo();
            mapData.NextBuildingTypeForCreation = _nextBuildingTypeForCreation;
            mapData.NextBuildingForCreationBuildsCount = _nextBuildingForCreationBuildsCount;
            mapData.UpdateAvailableBuildingForCreation(_availableBuildingsForCreation);
        }
    }
}
