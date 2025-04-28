using Assets.Scripts.Services.PersistantProgrssService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens.ScreenStart.SettingsPanel
{
    public abstract class SettingsOption : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;

        protected IPersistantProgrss PersistentProgressService { get; private set; }

        protected Toggle Toggle => _toggle;

        [Inject]
        private void Construct(IPersistantProgrss persistentProgressService)
        {
            PersistentProgressService = persistentProgressService;

            _toggle.isOn = SetUpToggle();

            _toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        private void OnDestroy()
        {
            _toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }

        protected abstract void OnToggleValueChanged(bool value);

        protected abstract bool SetUpToggle();
    }
}
