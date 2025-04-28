using Assets.Scripts.Data.Map;
using Assets.Scripts.Services.StaticDataServices.Configs.Reward;
using Assets.Scripts.UI.Screens.Map.Reward;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs
{
    [CreateAssetMenu(fileName = "RewardConfig", menuName = "StaticData/Reward/Create new reward config", order = 51)]
    public class RewardConfig : ScriptableObject
    {
        public RewardType Type;
        public RewardPanel PrefabPanel;
        public Sprite IconAssetReference;
        public uint ProportionOfLoss;
        public int MinCount;
        public int MaxCount;

        public virtual uint GetRewardCount(IMapData mapData)
        {
            return (uint)Random.Range(MinCount, MaxCount);
        }
    }
}