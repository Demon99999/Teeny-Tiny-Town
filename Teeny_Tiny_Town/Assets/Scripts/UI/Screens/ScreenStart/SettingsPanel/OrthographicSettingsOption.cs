namespace Assets.Scripts.UI.Screens.ScreenStart.SettingsPanel
{
    public class OrthographicSettingsOption : SettingsOption
    {
        protected override void OnToggleValueChanged(bool value)
        {
            PersistentProgressService.Progress.SettingsData.ChangeOrthographic(value);
        }

        protected override bool SetUpToggle()
        {
            return PersistentProgressService.Progress.SettingsData.IsOrthographicCamera;
        }
    }
}
