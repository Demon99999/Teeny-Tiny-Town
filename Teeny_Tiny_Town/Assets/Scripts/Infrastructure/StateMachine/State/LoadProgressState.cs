using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.Data.Map;
using Assets.Scripts.Infrastructure.State;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.SaveLoadServices;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;
using UnityEngine.InputSystem.Utilities;

namespace Assets.Scripts.Infrastructure.StateMachine.State
{
    public class LoadProgressState : IState
    {
        private const uint StarGamplayWalletValue = 10000;

        private readonly GameStateMachine stateMachine;
        private readonly IPersistantProgrss _progressService;
        private readonly IStaticDataService _staticDataService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine stateMachine, IPersistantProgrss progress, IStaticDataService staticDataService, ISaveLoadService saveLoadService)
        {
            this.stateMachine = stateMachine;
            _progressService = progress;
            _staticDataService = staticDataService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();

            stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {

        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = _saveLoadService.LoadProgres() ?? CreateNewProgress();
        }

        private PlayerProgres CreateNewProgress()
        {
            List<QuestData> startQuests = new List<QuestData>();
            startQuests.AddRange(_staticDataService.QuestsConfig.StartQuestsId.Select(id => new QuestData(id)));

            PlayerProgres progress = new PlayerProgres(
                GetWorldDatas(),
                startQuests,
                _staticDataService.MapsConfig.AvailableMovesCount,
                _staticDataService.SandboxConfig.Size,
                _staticDataService.GetAllBuildings(),
                _staticDataService.MapsConfig.EducationMapdId,
                StarGamplayWalletValue);

            return progress;
        }

        private MapData[] GetWorldDatas()
        {
            ReadOnlyArray<MapConfig> mapConfigs = _staticDataService.MapConfigs;

            MapData[] mapDatas = new MapData[mapConfigs.Count];

            for (int i = 0; i < mapConfigs.Count; i++)
            {
                MapConfig config = mapConfigs[i];
                mapDatas[i] = config.GetWorldData(_staticDataService.MapsConfig.Goals, _staticDataService);
            }

            return mapDatas;
        }
    }
}