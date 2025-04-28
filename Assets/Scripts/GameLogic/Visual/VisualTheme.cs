using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic.Visual
{
    public abstract class VisualTheme : MonoBehaviour
    {
        protected Tween ThemeChanger;

        protected IPersistantProgrss PersistentProgressService { get; private set; }
        protected AnimationsConfig AnimationsConfig { get; private set; }

        [Inject]
        private void Construct(IPersistantProgrss persistentProgressService, IStaticDataService staticDataService)
        {
            PersistentProgressService = persistentProgressService;
            AnimationsConfig = staticDataService.AnimationsConfig;

            PersistentProgressService.Progress.SettingsData.ThemeChanged += ChangeTheme;
        }

        private void OnDestroy()
        {
            PersistentProgressService.Progress.SettingsData.ThemeChanged -= ChangeTheme;
            ThemeChanger?.Kill();
        }

        protected abstract void ChangeTheme();
    }
}
