namespace Assets.Scripts.UI.Screens.ScreenStart.SettingsPanel
{
    public class DarkThemeOption : SettingsOption
    {
        private void OnEnable()
        {
            PersistentProgressService.Progress.SettingsData.ThemeChanged += OnThemeChanged;
        }

        private void OnDisable()
        {
            PersistentProgressService.Progress.SettingsData.ThemeChanged -= OnThemeChanged;
        }

        protected override void OnToggleValueChanged(bool value)
        {
            PersistentProgressService.Progress.SettingsData.ChangeTheme(value);
        }

        protected override bool SetUpToggle()
        {
            return PersistentProgressService.Progress.SettingsData.IsDarkTheme;
        }

        private void OnThemeChanged()
        {
            Toggle.isOn = PersistentProgressService.Progress.SettingsData.IsDarkTheme;
        }
    }
}
