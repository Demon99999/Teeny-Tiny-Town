using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using Assets.Scripts.Infrastructure.AssetPro;
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
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

namespace Assets.Scripts.Services.StaticDataServices
{
    public class StaticDataServices : IStaticDataService
    {
        private readonly IAssetProvider _assetsProvider;

        private Dictionary<BuildingType, BuildingConfig> _buildingConfigs;
        private Dictionary<GroundType, Dictionary<RoadType, RoadConfig>> _groundConfigs;
        private Dictionary<ScreenType, ScreenConfig> _screenConfigs;
        private Dictionary<BuildingType, BuildingStoreItemConfig> _worldStoreItemConfigs;
        private Dictionary<BuildingType, GroundType> _roadGrounds;
        private Dictionary<string, MapConfig> _mapConfigs;
        private Dictionary<RewardType, RewardConfig> _rewardConfigs;
        private Dictionary<GameplayStoreItemType, StoreItemConfig> _gameplayStoreItemConfigs;
        private Dictionary<AdditionalBonusType, AdditionalBonusConfig> _additionalBonusConfigs;
        private Dictionary<GainStoreItemType, GainStoreItemConfig> _gainStoreItemConfigs;
        private Dictionary<TileType, GroundDefaultSurfaceConfig> _defaultGroundConfigs;

        public StaticDataServices(IAssetProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }

        public AvailableForConstructionBuildingsConfig AvailableForConstructionBuildingsConfig { get; private set; }

        public MapsConfig MapsConfig { get; private set; }

        public ReadOnlyArray<MapConfig> MapConfigs => _mapConfigs.Values.ToArray();

        public BuildingStoreItemsCofnig StoreItemsConfig { get; private set; }

        public AnimationsConfig AnimationsConfig { get; private set; }

        public GroundsConfig GroundsConfig { get; private set; }

        public QuestsConfig QuestsConfig { get; private set; }

        public SandboxConfig SandboxConfig { get; private set; }

        public void Initialize()
        {
            LoadMapsConfig();
            LoadGainStoreItemConfigs();
            LoadQuestsConfig();
            LoadBuildingConfigs();
            LoadGroundsConfig();
            LoadMapConfigs();
            LoadAnimationsConfig();
            LoadScreensConfig();
            LoadRoadGroundConfigs();
            LoadSandboxConfig();
            LoadAdditionalBonusConfigs();
            LoadRewardConfigs();
            LoadWorldStoreItemConfigs();
            LoadAvailableForConstructionBuildingsConfig();
            LoadGameplayStoreItemConfigs();
        }

        public BuildingType[] GetAllBuildings()
        {
            return _buildingConfigs.Keys.OrderBy(type => (int)type).ToArray();
        }

        public TMapConfig GetMap<TMapConfig>(string id)
            where TMapConfig : MapConfig
        {
           return _mapConfigs.TryGetValue(id, out MapConfig config) ? config as TMapConfig : null;
        }

        public ScreenConfig GetScreen(ScreenType type)
        {
            return _screenConfigs.TryGetValue(type, out ScreenConfig config) ? config : null;
        }

        public GroundDefaultSurfaceConfig GetDefaultGround(TileType tileType)
        {
            return _defaultGroundConfigs.TryGetValue(tileType, out GroundDefaultSurfaceConfig config) ? config : null;
        }

        public GainStoreItemConfig GetGainStoreItem(GainStoreItemType gainStoreItemType)
        {
            return _gainStoreItemConfigs.TryGetValue(gainStoreItemType, out GainStoreItemConfig config) ? config : null;
        }

        public AdditionalBonusConfig GetAdditionalBonus(AdditionalBonusType type)
        {
            return _additionalBonusConfigs.TryGetValue(type, out AdditionalBonusConfig config) ? config : null;
        }

        public TBuilding GetBuilding<TBuilding>(BuildingType buildingType)
            where TBuilding : BuildingConfig
        {
            return _buildingConfigs.TryGetValue(buildingType, out BuildingConfig config) ? config as TBuilding : null;
        }

        public BuildingStoreItemConfig GetBuildingStoreItem(BuildingType buildingType)
        {
            return _worldStoreItemConfigs.TryGetValue(buildingType, out BuildingStoreItemConfig config) ? config : null;
        }

        public RoadConfig GetRoad(GroundType groundType, RoadType roadType)
        {
            return _groundConfigs.TryGetValue(
                groundType, out Dictionary<RoadType, RoadConfig> roadConfigs) ? (roadConfigs.TryGetValue(
                roadType, out RoadConfig config) ? config : null) : null;
        }

        public GroundType GetGroundType(BuildingType buildingType)
        {
            return _roadGrounds.TryGetValue(buildingType, out GroundType type) ? type : default;
        }

        public RewardConfig GetReward(RewardType type)
        {
            return _rewardConfigs.TryGetValue(type, out RewardConfig config) ? config : null;
        }

        public StoreItemConfig GetGameplayStorItem(GameplayStoreItemType type)
        {
            return _gameplayStoreItemConfigs.TryGetValue(type, out StoreItemConfig config) ? config : null;
        }

        private void LoadMapsConfig()
        {
            MapsConfig mapsConfigs = GetConfig<MapsConfig>(StaticDataConfigsPath.MapsConfig);

            MapsConfig = mapsConfigs;
        }

        private void LoadScreensConfig()
        {
            ScreensConfig screensConfig = GetConfig<ScreensConfig>(StaticDataConfigsPath.ScreensConfig);
            _screenConfigs = screensConfig.Configs.ToDictionary(value => value.Type, value => value);
        }

        private void LoadGroundsConfig()
        {
            GroundsConfig groundsConfigs = GetConfig<GroundsConfig>(StaticDataConfigsPath.GroundsConfig);

            GroundsConfig = groundsConfigs;
            _groundConfigs = GroundsConfig.GroundConfigs.ToDictionary(value => value.Type, val => val.RoadConfigs.ToDictionary(value => value.Type, value => value));
            _defaultGroundConfigs = GroundsConfig.DefaultGroundConfigs.ToDictionary(value => value.TileType, value => value);
        }

        private void LoadRoadGroundConfigs()
        {
            RoadGroundConfigs roadGroundConfigs = GetConfig<RoadGroundConfigs>(StaticDataConfigsPath.RoadGroundConfigs);

            _roadGrounds = roadGroundConfigs.Configs.ToDictionary(value => value.BuildingType, value => value.GroundType);
        }

        private void LoadAnimationsConfig()
        {
            AnimationsConfig configs = GetConfig<AnimationsConfig>(StaticDataConfigsPath.AnimationsConfig);

            AnimationsConfig = configs;
        }

        private void LoadQuestsConfig()
        {
            QuestsConfig configs = GetConfig<QuestsConfig>(StaticDataConfigsPath.QuestsConfig);

            QuestsConfig = configs;
        }

        private void LoadSandboxConfig()
        {
            SandboxConfig sandboxConfigs = GetConfig<SandboxConfig>(StaticDataConfigsPath.SandboxConfig);

            SandboxConfig = sandboxConfigs;
        }

        private void LoadWorldStoreItemConfigs()
        {
            BuildingStoreItemsCofnig storeItemsConfigs = GetConfig<BuildingStoreItemsCofnig>(StaticDataConfigsPath.BildingStoreItemsConfig);

            StoreItemsConfig = storeItemsConfigs;

            _worldStoreItemConfigs = StoreItemsConfig.Configs.ToDictionary(value => value.BuildingType, value => value);
        }

        private void LoadAvailableForConstructionBuildingsConfig()
        {
            AvailableForConstructionBuildingsConfig configs = GetConfig<AvailableForConstructionBuildingsConfig>(StaticDataConfigsPath.AvailableForConstructionBuildingsConfig);

            AvailableForConstructionBuildingsConfig = configs;
        }

        private void LoadBuildingConfigs()
        {
            BuildingConfig[] buildingConfigs = GetConfigs<BuildingConfig>(StaticDataConfigsPath.BuildingConfigs);

            _buildingConfigs = buildingConfigs.ToDictionary(value => value.BuildingType, value => value);
        }

        private void LoadGainStoreItemConfigs()
        {
            GainStoreItemConfig[] configs = GetConfigs<GainStoreItemConfig>(StaticDataConfigsPath.GainStoreConfigs);

            _gainStoreItemConfigs = configs.ToDictionary(value => value.Type, value => value);
        }

        private void LoadMapConfigs()
        {
            MapConfig[] configs = GetConfigs<MapConfig>(StaticDataConfigsPath.MapConfig);
            _mapConfigs = configs.ToDictionary(value => value.Id, value => value);
        }

        private void LoadRewardConfigs()
        {
            RewardConfig[] configs = GetConfigs<RewardConfig>(StaticDataConfigsPath.RewardConfigs);

            _rewardConfigs = configs.ToDictionary(value => value.Type, value => value);
        }

        private void LoadAdditionalBonusConfigs()
        {
            AdditionalBonusConfig[] configs = GetConfigs<AdditionalBonusConfig>(StaticDataConfigsPath.AdditionalBonusConfigs);

            _additionalBonusConfigs = configs.ToDictionary(value => value.Type, value => value);
        }

        private void LoadGameplayStoreItemConfigs()
        {
            StoreItemConfig[] configs = GetConfigs<StoreItemConfig>(StaticDataConfigsPath.GameplayStoreItemConfigs);

            _gameplayStoreItemConfigs = configs.ToDictionary(value => value.Type, value => value);
        }

        private TConfig GetConfig<TConfig>(string path)
            where TConfig : Object
        {
            return _assetsProvider.Load<TConfig>(path);
        }

        private TConfigs[] GetConfigs<TConfigs>(string path)
            where TConfigs : Object
        {
            return _assetsProvider.LoadAll<TConfigs>(path);
        }
    }
}