using System;
using Assets.Scripts.Services.PersistantProgrssService;
using UnityEngine.Audio;

namespace Assets.Scripts.Audio
{
    public class AudioMixerChanger : IDisposable
    {
        private const string MusicVolume = "MusicVolume";
        private const string SoundsVolume = "SoundsVolume";
        private const int AudioOnValue = 0;
        private const int AudioOffValue = -80;

        private readonly IPersistantProgrss _persistentProgressService;
        private readonly AudioMixer _audioMixer;

        public AudioMixerChanger(IPersistantProgrss persistentProgressService, AudioMixer audioMixer)
        {
            _persistentProgressService = persistentProgressService;
            _audioMixer = audioMixer;

            ChangeMixer();

            _persistentProgressService.Progress.SettingsData.AudioChanged += ChangeMixer;
        }

        public void Dispose()
        {
            _persistentProgressService.Progress.SettingsData.AudioChanged -= ChangeMixer;
        }

        private void ChangeMixer()
        {
            _audioMixer.SetFloat(MusicVolume, _persistentProgressService.Progress.SettingsData.IsMusicOn ? AudioOnValue : AudioOffValue);
            _audioMixer.SetFloat(SoundsVolume, _persistentProgressService.Progress.SettingsData.IsSoundsOn ? AudioOnValue : AudioOffValue);
        }
    }
}
