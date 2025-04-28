using System;
using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Mover;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;

namespace Assets.Scripts.GameLogic
{
    public class WorldExpander : IDisposable
    {
        private readonly IMapData _worldData;
        private readonly ExpandingMapConfig _expandingWorldConfig;
        private readonly IExpandingGameplayMover _expandingGameplayMover;

        public WorldExpander(
            IStaticDataService staticDataService,
            IMapData worldData,
            IExpandingGameplayMover expandingGameplayMover)
        {
            _worldData = worldData;
            _expandingWorldConfig = staticDataService.GetMap<ExpandingMapConfig>(worldData.Id);
            _expandingGameplayMover = expandingGameplayMover;

            _worldData.BuildingUpgraded += OnBuildingUpdated;
        }

        public void Dispose()
        {
            _worldData.BuildingUpgraded -= OnBuildingUpdated;
        }

        private void OnBuildingUpdated(BuildingType type)
        {
            if (_expandingWorldConfig.ContainsExpand(type, out ExpandConfig expandConfig) &&
                expandConfig.ExpandedSize.magnitude > _worldData.Size.magnitude)
            {
                _expandingGameplayMover.ExpandWorld(expandConfig.ExpandedSize);
            }
        }
    }
}
