using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs.Buildings
{
    [CreateAssetMenu(fileName = "BuildingConfig", menuName = "StaticData/Building/Create new building config", order = 51)]
    public class BuildingConfig : ScriptableObject
    {
        [SerializeField] private BuildingType _buildingType;
        [SerializeField] private uint _pointsRewardForMerge;
        [SerializeField] private string _name;
        [SerializeField] private string _title;
        [SerializeField] private uint _proportionOfLoss;
        [SerializeField] private Sprite _lockIconAssetReference;
        [SerializeField] private Sprite _iconAssetReference;
        [SerializeField] private BuildingRepresentation _prefab;

        public BuildingType BuildingType => _buildingType;

        public uint PointsRewardForMerge => _pointsRewardForMerge;

        public string Name => _name;

        public string Title => _title;

        public uint ProportionOfLoss => _proportionOfLoss;

        public Sprite LockIconAssetReference => _lockIconAssetReference;

        public Sprite IconAssetReference => _iconAssetReference;

        public BuildingRepresentation Prefab => _prefab;
    }
}