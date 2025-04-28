using System;
using System.Linq;
using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;

namespace Assets.Scripts.GameLogic.Map
{
    public class WorldCleaner : IDisposable
    {
        private readonly Map _map;
        private readonly IMapData _mapData;
        private readonly IStaticDataService _staticDataService;
        private readonly IWorldChanger _worldChanger;

        public WorldCleaner(Map map, IMapData mapData, IStaticDataService staticDataService, IWorldChanger worldChanger)
        {
            _map = map;

            _map.Cleaned += OnMapCleaned;
            _mapData = mapData;
            _staticDataService = staticDataService;
            _worldChanger = worldChanger;
        }

        public void Dispose()
        {
            _map.Cleaned -= OnMapCleaned;
        }

        private void OnMapCleaned()
        {
            MapConfig mapConfig = _staticDataService.GetMap<MapConfig>(_mapData.Id);

            _mapData.IsChangingStarted = false;
            _mapData.Update(mapConfig.TilesDatas, mapConfig.NextBuildingTypeForCreation, mapConfig.StartingAvailableBuildingTypes.ToList());
            _worldChanger.Update(true);
        }
    }
}
