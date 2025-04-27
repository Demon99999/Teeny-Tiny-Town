using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using Assets.Scripts.Services.StaticDataServices.Configs.Reward;

namespace Assets.Scripts.GameLogic.Points
{
    public class Rewarder
    {
        private readonly IMapData _worldData;
        private readonly NextBuildingForPlacingCreator _nextBuildingForPlacingCreator;

        public Rewarder(IMapData worldData, NextBuildingForPlacingCreator nextBuildingForPlacingCreator)
        {
            _worldData = worldData;
            _nextBuildingForPlacingCreator = nextBuildingForPlacingCreator;
        }

        public void Reward(RewardType rewardType, uint rewardCount)
        {
            switch (rewardType)
            {
                case RewardType.ReplaceItem:
                    _worldData.ReplaceItems.AddItems(rewardCount);
                    break;
                case RewardType.Bulldozer:
                    _worldData.BulldozerItems.AddItems(rewardCount);
                    break;
                case RewardType.Crane:
                    _nextBuildingForPlacingCreator.ChangeCurrentBuildingForPlacing(BuildingType.Crane);
                    break;
                case RewardType.Lighthouse:
                    _nextBuildingForPlacingCreator.ChangeCurrentBuildingForPlacing(BuildingType.Lighthouse);
                    break;
                case RewardType.WorldWalletValue:
                    AddWorldWalletValue(rewardCount);
                    break;
            }
        }

        private void AddWorldWalletValue(uint count)
        {
            if (_worldData is ICurrencyMapData currencyWorldData)
            {
                currencyWorldData.WorldWallet.Give(count);
            }
        }
    }
}
