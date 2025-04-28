using System;
using System.Collections.Generic;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

namespace Assets.Scripts.Data.Map
{
    public interface IMapData
    {
        event Action<BuildingType> BuildingUpgraded;

        ReadOnlyArray<TileData> Tiles { get; }
        Vector2Int Size { get; set; }
        IReadOnlyList<BuildingType> AvailableBuildingsForCreation { get; }
        string Id { get; }
        BuildingType NextBuildingTypeForCreation { get; set; }
        uint NextBuildingForCreationBuildsCount { get; set; }
        bool IsChangingStarted { get; set; }
        PointsData PointsData { get; }
        UpgradeData BulldozerItems { get; }
        UpgradeData ReplaceItems { get; }
        BuildingType[] Inventory { get; }

        void TryAddBuildingTypeForCreation(BuildingType createdBuilding, uint requiredCreatedBuildingsToAddNext, IStaticDataService staticDataService);
        void UpdateTileDatas(TileData[] tileDatas);
        void UpdateAvailableBuildingForCreation(IReadOnlyList<BuildingType> availableBuildingsForCreation);
        void Update(TileData[] tiles, BuildingType nextBuildingTypeForCreation, List<BuildingType> availableBuildingsForCreation);
    }
}