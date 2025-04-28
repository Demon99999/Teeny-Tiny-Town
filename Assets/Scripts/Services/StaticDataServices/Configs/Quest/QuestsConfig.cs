using System.Linq;
using Assets.Scripts.UI.Screens.Map.Quest;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs.Quest
{
    [CreateAssetMenu(fileName = "QuestsConfig", menuName = "StaticData/Create new quests config", order = 51)]
    public class QuestsConfig : ScriptableObject
    {
        public QuestConfig[] Configs;
        public string[] StartQuestsId;
        public QuestPanel QuestPanel;

        public QuestConfig GetQuest(string id)
        {
            return Configs.First(config => config.Id == id);
        }
    }
}