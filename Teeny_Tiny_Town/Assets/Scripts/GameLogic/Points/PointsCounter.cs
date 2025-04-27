using System;
using Assets.Scripts.Data.Map;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;

namespace Assets.Scripts.GameLogic.Points
{
    public class PointsCounter : IDisposable
    {
        private readonly IMapData _mapData;
        private readonly IStaticDataService _staticDataService;

        public PointsCounter(IMapData mapData, IStaticDataService staticDataService)
        {
            _mapData = mapData;
            _staticDataService = staticDataService;
            _mapData.BuildingUpgraded += OnBuildingUpdated;
        }

        public void Dispose()
        {
            _mapData.BuildingUpgraded -= OnBuildingUpdated;
        }

        private void OnBuildingUpdated(BuildingType type)
        {
            _mapData.PointsData.Give(_staticDataService.GetBuilding<BuildingConfig>(type).PointsRewardForMerge);
        }
    }
}
