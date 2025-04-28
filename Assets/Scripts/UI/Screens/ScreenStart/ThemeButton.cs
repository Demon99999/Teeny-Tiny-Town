using Assets.Scripts.Services.PersistantProgrssService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens.ScreenStart
{
    public class ThemeButton : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Sprite _sunIcon;
        [SerializeField] private Sprite _moonIcon;
        [SerializeField] private Button _button;

        private IPersistantProgrss _persistentProgressService;

        [Inject]
        private void Construct(IPersistantProgrss persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;

            ChangeThemeIcon();

            _persistentProgressService.Progress.SettingsData.ThemeChanged += ChangeThemeIcon;
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDestroy()
        {
            _persistentProgressService.Progress.SettingsData.ThemeChanged -= ChangeThemeIcon;
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            _persistentProgressService.Progress.SettingsData.ChangeTheme(_persistentProgressService.Progress.SettingsData.IsDarkTheme == false);
        }

        private void ChangeThemeIcon()
        {
            _icon.sprite = _persistentProgressService.Progress.SettingsData.IsDarkTheme ? _moonIcon : _sunIcon;
        }
    }
}
