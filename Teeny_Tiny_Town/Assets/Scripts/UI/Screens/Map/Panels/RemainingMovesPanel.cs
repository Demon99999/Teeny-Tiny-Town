using Assets.Scripts.Services.PersistantProgrssService;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map.Panels
{
    public class RemainingMovesPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _remainingMovesCountValue;

        private IPersistantProgrss _persistentProgressService;

        [Inject]
        public void Construct(IPersistantProgrss persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;

            _persistentProgressService.Progress.GameplayMovesCounter.RemainingMovesCountChanged += OnRemainingMovesCountChanged;

            OnRemainingMovesCountChanged();
        }

        private void OnDestroy()
        {
            _persistentProgressService.Progress.GameplayMovesCounter.RemainingMovesCountChanged -= OnRemainingMovesCountChanged;
        }

        private void OnRemainingMovesCountChanged()
        {
            _remainingMovesCountValue.text = _persistentProgressService.Progress.GameplayMovesCounter.RemainingMovesCount.ToString();
        }

        public class Factory : PlaceholderFactory<RemainingMovesPanel> { }
    }
}
