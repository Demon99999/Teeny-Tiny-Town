using Assets.Scripts.Data;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.SaveLoadServices;
using UnityEngine;

namespace Assets.Scripts.Services.SaveLoadSerices
{
    public class SaveLoadServices : ISaveLoadService
    {
        private const string Key = "Progress";

        private readonly IPersistantProgrss _progressService;

        public SaveLoadServices(IPersistantProgrss progressService)
        {
            _progressService = progressService;
        }

        public PlayerProgres LoadProgres()
        {
            PlayerProgres playerProgres = PlayerPrefs.GetString(Key)?.ToDeserialized<PlayerProgres>();

            return playerProgres;
        }

        public void SaveProgress()
        {
            PlayerPrefs.SetString(Key, _progressService.Progress.ToJson());
        }
    }
}