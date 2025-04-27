using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using Assets.Scripts.Services.StaticDataServices.Configs;
using Assets.Scripts.Services.StaticDataServices.Configs.AdditionalBonuses;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using Assets.Scripts.Services.StaticDataServices.Configs.GameStore;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps.SandBox;
using Assets.Scripts.Services.StaticDataServices.Configs.Quest;
using Assets.Scripts.Services.StaticDataServices.Configs.Reward;
using Assets.Scripts.Services.StaticDataServices.Configs.Screens;
using Assets.Scripts.Services.StaticDataServices.Configs.Store;
using UnityEngine.InputSystem.Utilities;

namespace Assets.Scripts.Services.StaticDataServices
{
    public interface IStaticDataService
    {
        GroundsConfig GroundsConfig { get; }
        BuildingStoreItemsCofnig StoreItemsConfig { get; }
        MapsConfig MapsConfig { get; }
        AvailableForConstructionBuildingsConfig AvailableForConstructionBuildingsConfig { get; }
        ReadOnlyArray<MapConfig> MapConfigs { get; }
        AnimationsConfig AnimationsConfig { get; }
        QuestsConfig QuestsConfig { get; }
        SandboxConfig SandboxConfig { get; }

        void Initialize();
        RoadConfig GetRoad(GroundType groundType, RoadType roadType);
        ScreenConfig GetScreen(ScreenType type);
        BuildingStoreItemConfig GetBuildingStoreItem(BuildingType buildingType);
        GroundType GetGroundType(BuildingType buildingType);
        TMapConfig GetMap<TMapConfig>(string id) where TMapConfig : MapConfig;
        GainStoreItemConfig GetGainStoreItem(GainStoreItemType gainStoreItemType);
        RewardConfig GetReward(RewardType type);
        StoreItemConfig GetGameplayStorItem(GameplayStoreItemType type);
        AdditionalBonusConfig GetAdditionalBonus(AdditionalBonusType type);
        BuildingType[] GetAllBuildings();
        TBuilding GetBuilding<TBuilding>(BuildingType buildingType) where TBuilding : BuildingConfig;
        GroundDefaultSurfaceConfig GetDefaultGround(TileType tileType);
    }
}