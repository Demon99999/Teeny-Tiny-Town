using System.Linq;
using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map;
using Assets.Scripts.Services.StaticDataServices.Configs.AdditionalBonuses;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using Assets.Scripts.Services.StaticDataServices.Configs.Reward;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs.Maps
{
    [CreateAssetMenu(fileName = "MapConfig", menuName = "StaticData/MapConfig/Create new map config", order = 51)]
    public class MapConfig : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private string _nextWorldId;
        [SerializeField] private string _previousWorldId;
        [SerializeField] private Vector2Int _size;
        [SerializeField] private TileConfig[] _tileConfigs;
        [SerializeField] private BuildingType _nextBuildingTypeForCreation;
        [SerializeField] private Map _mapTemplate;
        [SerializeField] private BuildingType[] _startingAvailableBuildingTypes;
        [SerializeField] private RewardType[] _availableRewards;
        [SerializeField] private AdditionalBonusType[] _availableAdditionalBonuses;
        [SerializeField] private int _minRewardVariantsCount;
        [SerializeField] private int _maxRewardVariantsCount;
        [SerializeField] private bool _isUnlockedOnStart;
        [SerializeField] private uint _cost;
        [SerializeField] private string _name;
        [SerializeField] private Sprite[] _peculiarityIcon;

        public string Id => _id;

        public string NextWorldId => _nextWorldId;

        public string PreviousWorldId => _previousWorldId;

        public Vector2Int Size
        {
            get => _size;
            set => _size = value;
        }

        public TileConfig[] TileConfigs
        {
            get => _tileConfigs;
            set => _tileConfigs = value;
        }

        public BuildingType NextBuildingTypeForCreation => _nextBuildingTypeForCreation;

        public Map MapTemplate => _mapTemplate;

        public BuildingType[] StartingAvailableBuildingTypes => _startingAvailableBuildingTypes;

        public RewardType[] AvailableRewards => _availableRewards;

        public AdditionalBonusType[] AvailableAdditionalBonuses => _availableAdditionalBonuses;

        public int MinRewardVariantsCount => _minRewardVariantsCount;

        public int MaxRewardVariantsCount => _maxRewardVariantsCount;

        public bool IsUnlockedOnStart => _isUnlockedOnStart;

        public uint Cost => _cost;

        public string Name => _name;

        public Sprite[] PeculiarityIcon => _peculiarityIcon;

        public TileData[] TilesDatas => TileConfigs.Select(tileConfig => new TileData(tileConfig.GridPosition, tileConfig.BuildingType)).ToArray();

        public TileType GetTileType(Vector2Int gridPosition)
        {
            return TileConfigs.First(tile => tile.GridPosition == gridPosition).Type;
        }

        private void OnValidate()
        {
            CreateTileConfigs();
        }

        public virtual MapData GetWorldData(uint[] goals, IStaticDataService staticDataService)
        {
            return new MapData(Id, TilesDatas, NextBuildingTypeForCreation, StartingAvailableBuildingTypes.ToList(), Size, goals, IsUnlockedOnStart);
        }

        private void CreateTileConfigs()
        {
            TileConfig[] newTileConfigs = new TileConfig[Size.x * Size.y];

            if (TileConfigs == null)
            {
                int i = 0;

                for (int x = 0; x < Size.x; x++)
                {
                    for (int z = 0; z < Size.y; z++)
                    {
                        newTileConfigs[i] = new TileConfig(new Vector2Int(x, z));
                        i++;
                    }
                }

                TileConfigs = newTileConfigs;
            }
            else
            {
                int i = 0;

                for (int x = 0; x < Size.x; x++)
                {
                    for (int z = 0; z < Size.y; z++)
                    {
                        if (i >= TileConfigs.Length)
                        {
                            newTileConfigs[i] = new TileConfig(new Vector2Int(x, z));
                        }
                        else
                        {
                            newTileConfigs[i] = TileConfigs[i];
                        }

                        i++;
                    }
                }

                TileConfigs = newTileConfigs;
            }
        }
    }
}
