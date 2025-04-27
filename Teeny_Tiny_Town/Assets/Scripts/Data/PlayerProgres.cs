using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Data.Map;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class PlayerProgres
    {
        public List<MapData> MapDatas;
        public List<CurrencyMapData> CurrencyMapDatas;
        public StoreData StoreData;
        public Wallet Wallet;
        public List<QuestData> Quests;
        public GameplayMovesCounterData GameplayMovesCounter;
        public SandboxData SandboxData;
        public BuildingData[] BuildingDatas;
        public bool IsEducationCompleted;
        public SettingsData SettingsData;
        public string LastPlayedWorldDataId;

        public PlayerProgres(
           MapData[] mapDatas,
           List<QuestData> quests,
           uint startRemainingMoveCount,
           Vector2Int sandboxSize,
           BuildingType[] allBuildings,
           string startWorldId,
           uint startGameplayWalletValue)
        {
            Quests = quests;
            StoreData = new StoreData();
            Wallet = new Wallet(startGameplayWalletValue);
            GameplayMovesCounter = new GameplayMovesCounterData(startRemainingMoveCount, StoreData);
            SandboxData = new SandboxData(sandboxSize);
            SettingsData = new SettingsData();
            LastPlayedWorldDataId = startWorldId;
            IsEducationCompleted = false;

            BuildingDatas = new BuildingData[allBuildings.Length];

            for (int i = 0; i < allBuildings.Length; i++)
                BuildingDatas[i] = new BuildingData(allBuildings[i]);

            MapDatas = new List<MapData>();
            CurrencyMapDatas = new List<CurrencyMapData>();

            foreach (MapData mapData in mapDatas)
            {
                if (mapData is CurrencyMapData)
                    CurrencyMapDatas.Add((CurrencyMapData)mapData);
                else
                    MapDatas.Add(mapData);
            }
        }

        public event Action<string, string> QuestChanged;

        public QuestData GetQuest(string id)
        {
            return Quests.First(data => data.Id == id);
        }

        public void AddBuildingToCollection(BuildingType type)
        {
            BuildingDatas.First(data => data.Type == type).Count++;
        }

        public MapData GetMapData(string id)
        {
            MapData mapData = MapDatas.FirstOrDefault(data => data.Id == id);

            if (mapData == null)
            {
                mapData = CurrencyMapDatas.FirstOrDefault(data => data.Id == id);
            }

            return mapData;
        }

        public void ChangeQuest(string changedQuestId, QuestData newQuest)
        {
            Quests.Remove(Quests.First(questData => questData.Id == changedQuestId));
            Quests.Add(newQuest);

            QuestChanged?.Invoke(changedQuestId, newQuest.Id);
        }
    }
}