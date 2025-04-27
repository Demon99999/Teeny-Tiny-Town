using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Quest;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map.Quest
{
    public class QuestsList : MonoBehaviour
    {
        private IPersistantProgrss _persistentPorgressService;
        private IUIFactory _uiFactory;
        private IStaticDataService _staticDataService;

        private List<QuestPanel> _questPanels;

        [Inject]
        private void Construct(IPersistantProgrss persistentProgressService, IUIFactory uiFactory, IStaticDataService staticDataService)
        {
            _persistentPorgressService = persistentProgressService;
            _uiFactory = uiFactory;
            _staticDataService = staticDataService;

            _questPanels = new List<QuestPanel>();

            foreach (QuestData questData in _persistentPorgressService.Progress.Quests)
            {
                CreateQuestPanel(questData.Id);
            }

            _persistentPorgressService.Progress.QuestChanged += OnQuestChanged;
        }

        private void OnDestroy()
        {
            foreach (QuestPanel questPanel in _questPanels)
            {
                questPanel.Clicked -= OnQuestPanelClicked;
            }

            _persistentPorgressService.Progress.QuestChanged -= OnQuestChanged;
        }

        private void OnQuestPanelClicked(QuestPanel questPanel)
        {
            QuestConfig questConfig = _staticDataService.QuestsConfig.GetQuest(questPanel.Id);

            _persistentPorgressService.Progress.Wallet.Give(questConfig.Reward);

            bool isUniqueQuest = false;
            string questId = string.Empty;

            while (isUniqueQuest == false)
            {
                QuestConfig[] questConfigs = _staticDataService.QuestsConfig.Configs;
                questId = questConfigs[Random.Range(0, questConfigs.Length + 1)].Id;

                if (_persistentPorgressService.Progress.Quests.Any(questData => questData.Id == questId) == false)
                {
                    isUniqueQuest = true;
                }
            }

            _persistentPorgressService.Progress.ChangeQuest(questPanel.Id, new QuestData(questId));
        }

        private void OnQuestChanged(string changedQuestId, string newQuestId)
        {
            QuestPanel questPanel = _questPanels.First(panel => panel.Id == changedQuestId);

            _questPanels.Remove(questPanel);
            questPanel.Clicked -= OnQuestPanelClicked;
            Destroy(questPanel.gameObject);

            CreateQuestPanel(newQuestId);
        }

        private void CreateQuestPanel(string id)
        {
            QuestPanel questPanel = _uiFactory.CreateQuestPanel(id, transform);

            _questPanels.Add(questPanel);
            questPanel.Clicked += OnQuestPanelClicked;
        }
    }
}
