using Assets.Scripts.GameLogic.Map;
using Assets.Scripts.GameLogic.StateMashine;
using Assets.Scripts.Services.PersistantProgrssService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens.ScreenStart.SettingsPanel
{
    public class TutorialButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private StartWindowPanel _settingsPanel;

        private MapSwitcher _worldsList;
        private IPersistantProgrss _persistentProgressService;
        private GameplayStateMachine _gameplayStateMachine;

        [Inject]
        private void Construct(MapSwitcher worldsList, IPersistantProgrss persistentProgressService, GameplayStateMachine gameplayStateMachine)
        {
            _worldsList = worldsList;
            _persistentProgressService = persistentProgressService;
            _gameplayStateMachine = gameplayStateMachine;

            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            _persistentProgressService.Progress.IsEducationCompleted = false;
            _settingsPanel.OpenNextPanel();
            _worldsList.ChangeToEducationWorld(callback: () => _gameplayStateMachine.Enter<GameplayLoopState, bool>(true));
        }
    }
}
