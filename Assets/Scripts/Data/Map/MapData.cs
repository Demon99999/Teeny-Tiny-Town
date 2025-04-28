using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

namespace Assets.Scripts.Data.Map
{
    [Serializable]
    public class MapData : IMapData
    {
        private const int InventorySize = 3;

        public string Id;
        public TileData[] Tiles;
        public BuildingType NextBuildingTypeForCreation;
        public uint NextBuildingForCreationBuildsCount;
        public List<BuildingType> AvailableBuildingsForCreation;
        public Vector2Int Size;
        public bool IsChangingStarted;
        public PointsData PointsData;
        public UpgradeData BulldozerItems;
        public UpgradeData ReplaceItems;
        public bool IsUnlocked;
        public BuildingType[] Inventory;

        public MapData(
            string id,
            TileData[] tiles,
            BuildingType nextBuildingTypeForCreation,
            List<BuildingType> availableBuildingForCreation,
            Vector2Int size,
            uint[] goals,
            bool isUnlocked)
        {
            Id = id;
            Tiles = tiles;
            NextBuildingTypeForCreation = nextBuildingTypeForCreation;
            AvailableBuildingsForCreation = availableBuildingForCreation;
            Size = size;
            IsUnlocked = isUnlocked;

            NextBuildingForCreationBuildsCount = 0;
            IsChangingStarted = false;
            PointsData = new PointsData(goals);
            BulldozerItems = new UpgradeData();
            ReplaceItems = new UpgradeData();
            Inventory = new BuildingType[InventorySize];
        }

        public event Action<BuildingType> BuildingUpgraded;

        string IMapData.Id => Id;

        BuildingType IMapData.NextBuildingTypeForCreation
        {
            get => NextBuildingTypeForCreation;
            set => NextBuildingTypeForCreation = value;
        }

        uint IMapData.NextBuildingForCreationBuildsCount
        {
            get => NextBuildingForCreationBuildsCount;
            set => NextBuildingForCreationBuildsCount = value;
        }

        ReadOnlyArray<TileData> IMapData.Tiles => Tiles;
        IReadOnlyList<BuildingType> IMapData.AvailableBuildingsForCreation => AvailableBuildingsForCreation;

        bool IMapData.IsChangingStarted
        {
            get => IsChangingStarted;
            set => IsChangingStarted = value;
        }

        Vector2Int IMapData.Size
        {
            get => Size;
            set => Size = value;
        }

        PointsData IMapData.PointsData => PointsData;
        UpgradeData IMapData.BulldozerItems => BulldozerItems;
        UpgradeData IMapData.ReplaceItems => ReplaceItems;

        BuildingType[] IMapData.Inventory => Inventory;

        public void TryAddBuildingTypeForCreation(BuildingType createdBuilding, uint requiredCreatedBuildingsToAddNext, IStaticDataService staticDataService)
        {
            BuildingUpgraded?.Invoke(createdBuilding);

            if (NextBuildingTypeForCreation != createdBuilding || NextBuildingTypeForCreation == BuildingType.Undefined)
                return;

            NextBuildingForCreationBuildsCount++;

            if (NextBuildingForCreationBuildsCount >= requiredCreatedBuildingsToAddNext)
            {
                if (staticDataService.AvailableForConstructionBuildingsConfig.TryFindeNextBuilding(createdBuilding, out BuildingType nextBuildingType))
                    AddNextBuildingTypeForCreation(nextBuildingType);
                else
                    NextBuildingTypeForCreation = BuildingType.Undefined;
            }
        }

        protected virtual void AddNextBuildingTypeForCreation(BuildingType type)
        {
            AvailableBuildingsForCreation.Add(NextBuildingTypeForCreation);
            NextBuildingTypeForCreation = type;
            NextBuildingForCreationBuildsCount = 0;
        }

        public void UpdateTileDatas(TileData[] targetTileDatas)
        {
            foreach (TileData targetTileData in targetTileDatas)
            {
                TileData tileData = Tiles.First(value => value.GridPosition == targetTileData.GridPosition);
                tileData.BuildingType = targetTileData.BuildingType;
            }
        }

        public void UpdateAvailableBuildingForCreation(IReadOnlyList<BuildingType> availableBuildingsForCreation)
        {
            AvailableBuildingsForCreation = availableBuildingsForCreation.Intersect(AvailableBuildingsForCreation).ToList();
        }

        public virtual void Update(TileData[] tiles, BuildingType nextBuildingTypeForCreation, List<BuildingType> availableBuildingsForCreation)
        {
            UpdateTileDatas(tiles);
            NextBuildingTypeForCreation = nextBuildingTypeForCreation;
            AvailableBuildingsForCreation = availableBuildingsForCreation;

            NextBuildingForCreationBuildsCount = 0;
            Inventory = new BuildingType[InventorySize];

            BulldozerItems.SetItemsCount(0);
            ReplaceItems.SetItemsCount(0);
        }
    }
}
