using System;
using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using Assets.Scripts.Services.StaticDataServices.Configs.Quest;

namespace Assets.Scripts.GameLogic
{
    public class QuestsChecker : IDisposable
    {
        private readonly IPersistantProgrss _persistentProgressService;
        private readonly IMapData _worldData;
        private readonly IWorldChanger _worldChanger;
        private readonly IStaticDataService _staticDataService;

        public QuestsChecker(
            IPersistantProgrss persistentProgressService,
            IMapData worldData,
            IWorldChanger worldChanger,
            IStaticDataService staticDataService)
        {
            _persistentProgressService = persistentProgressService;
            _worldData = worldData;
            _worldChanger = worldChanger;
            _staticDataService = staticDataService;

            _worldData.BuildingUpgraded += OnBuildingPlaced;
            _worldChanger.BuildingPlaced += OnBuildingPlaced;
        }

        public void Dispose()
        {
            _worldData.BuildingUpgraded -= OnBuildingPlaced;
            _worldChanger.BuildingPlaced -= OnBuildingPlaced;
        }

        private void OnBuildingPlaced(BuildingType type)
        {
            foreach (Data.QuestData questData in _persistentProgressService.Progress.Quests)
            {
                QuestConfig questConfig = _staticDataService.QuestsConfig.GetQuest(questData.Id);

                if (type == questConfig.BuildingType)
                {
                    questData.Perform(questConfig.TargetCount);
                }
            }
        }
    }
}
