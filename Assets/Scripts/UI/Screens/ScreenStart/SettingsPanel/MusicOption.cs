﻿using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.UI.Screens.ScreenStart.SettingsPanel
{
    public class MusicOption : SettingsOption
    {
        private const string MusicVolume = "MusicVolume";

        [SerializeField] private AudioMixer _mixer;

        protected override void OnToggleValueChanged(bool value)
        {
            _mixer.SetFloat(MusicVolume, value ? 0 : -80);
            PersistentProgressService.Progress.SettingsData.ChangeMusicActive(value);
        }

        protected override bool SetUpToggle()
        {
            _mixer.SetFloat(MusicVolume, PersistentProgressService.Progress.SettingsData.IsMusicOn ? 0 : -80);
            return PersistentProgressService.Progress.SettingsData.IsMusicOn;
        }
    }
}
