using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.UI.Screens.ScreenStart.SettingsPanel
{
    public class SoundsOption : SettingsOption
    {
        private const string SoundsVolume = "SoundsVolume";

        [SerializeField] private AudioMixer _mixer;

        protected override void OnToggleValueChanged(bool value)
        {
            _mixer.SetFloat(SoundsVolume, value ? 0 : -80);
            PersistentProgressService.Progress.SettingsData.ChangeSoundsActive(value);
        }

        protected override bool SetUpToggle()
        {
            _mixer.SetFloat(SoundsVolume, PersistentProgressService.Progress.SettingsData.IsSoundsOn ? 0 : -80);
            return PersistentProgressService.Progress.SettingsData.IsSoundsOn;
        }
    }
}