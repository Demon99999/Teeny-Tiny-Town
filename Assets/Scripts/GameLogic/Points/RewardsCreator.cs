using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Data.Map;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;
using Assets.Scripts.Services.StaticDataServices.Configs.Reward;
using Random = UnityEngine.Random;

namespace Assets.Scripts.GameLogic.Points
{
    public class RewardsCreator
    {
        private readonly IMapData _mapData;
        private readonly IStaticDataService _staticDataService;

        public RewardsCreator(IMapData mapData, IStaticDataService staticDataService)
        {
            _mapData = mapData;
            _staticDataService = staticDataService;
        }

        public event Action<IReadOnlyList<RewardType>> RewardsCreated;

        public void CreateRewards()
        {
            List<RewardType> rewards = new List<RewardType>();
            RewardType[] availableRewards = _staticDataService.GetMap<MapConfig>(_mapData.Id).AvailableRewards;

            int rewardVariansCount = GetRewardVariantsCount();

            for (int i = 0; i < rewardVariansCount; i++)
            {
                rewards.Add(GetRewards(availableRewards.Except(rewards).ToArray()));
            }

            RewardsCreated?.Invoke(rewards);
        }

        private int GetRewardVariantsCount()
        {
            MapConfig worldConfig = _staticDataService.GetMap<MapConfig>(_mapData.Id);

            return Random.Range(worldConfig.MinRewardVariantsCount, worldConfig.MaxRewardVariantsCount + 1);
        }

        private RewardType GetRewards(RewardType[] availableRewards)
        {
            RewardConfig[] rewardConfigs = availableRewards
                .Select(rewardType => _staticDataService.GetReward(rewardType))
                .OrderBy(rewardConfig => rewardConfig.ProportionOfLoss)
                .ToArray();

            int proportionsOfLossSum = (int)rewardConfigs.Sum(value => value.ProportionOfLoss);

            int resultChance = Random.Range(0, proportionsOfLossSum) + 1;
            uint chance = 0;

            for (int i = 0; i < rewardConfigs.Length; i++)
            {
                chance += rewardConfigs[i].ProportionOfLoss;

                if (resultChance <= chance)
                    return rewardConfigs[i].Type;
            }

            return RewardType.ReplaceItem;
        }
    }
}
