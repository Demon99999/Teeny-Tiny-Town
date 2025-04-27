using Assets.Scripts.Data.Map;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs.Reward
{
    [CreateAssetMenu(fileName = "WolrWalletValueRewardConfig", menuName = "StaticData/Reward/Create new world wallet value config", order = 51)]
    public class WorldWalletValueRewardConfig : RewardConfig
    {
        public float Multiplier;
        public uint MinReward;

        public override uint GetRewardCount(IMapData mapData)
        {
            if (mapData is ICurrencyMapData currencyWorldData)
            {
                uint reward = (uint)(Random.Range(MinCount, MaxCount + 1) * currencyWorldData.WorldWallet.Value * Multiplier);
                reward = reward < MinReward ? MinReward : reward;

                return reward;
            }

            return 0;
        }
    }
}