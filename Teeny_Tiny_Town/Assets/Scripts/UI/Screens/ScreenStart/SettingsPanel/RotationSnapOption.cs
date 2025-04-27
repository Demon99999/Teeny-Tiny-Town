namespace Assets.Scripts.UI.Screens.ScreenStart.SettingsPanel
{
    public class RotationSnapOption : SettingsOption
    {
        protected override void OnToggleValueChanged(bool value)
        {
            PersistentProgressService.Progress.SettingsData.IsRotationSnapped = value;
        }

        protected override bool SetUpToggle()
        {
            return PersistentProgressService.Progress.SettingsData.IsRotationSnapped;
        }
    }
}
