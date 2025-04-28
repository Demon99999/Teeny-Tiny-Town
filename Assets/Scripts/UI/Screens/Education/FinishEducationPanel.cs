using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.SaveLoadServices;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI.Screens.Education
{
    public class FinishEducationPanel : ContinueEducationPanel
    {
        [SerializeField] private CanvasGroup[] _showedUi;

        private IPersistantProgrss _persistentProgressService;
        private ISaveLoadService _saveLoadService;

        private uint _startMovesCount;

        [Inject]
        private void Construct(IPersistantProgrss presistentProgressService, ISaveLoadService saveLoadService)
        {
            _persistentProgressService = presistentProgressService;
            _saveLoadService = saveLoadService;

            _startMovesCount = _persistentProgressService.Progress.GameplayMovesCounter.RemainingMovesCount;
        }

        public override void Open()
        {
            foreach (CanvasGroup canvasGroup in _showedUi)
            {
                canvasGroup.DOFade(0, AnimationsConfig.WindowOpeningStateDuration).onComplete += () =>
                {
                    canvasGroup.blocksRaycasts = false;
                    canvasGroup.interactable = false;
                };
            }

            base.Open();
        }

        public override void Hide(TweenCallback callback)
        {
            Hide();
            base.Hide(callback);
        }

        public override void Hide()
        {
            InputService.SetEnabled(true);
            ActionHandlerStateMachine.SetActive(true);
            MarkersVisibility.ChangeAllowedVisibility(true);

            _persistentProgressService.Progress.IsEducationCompleted = true;

            foreach (CanvasGroup canvasGroup in _showedUi)
            {
                canvasGroup.DOFade(1, AnimationsConfig.WindowOpeningStateDuration).onComplete += () =>
                {
                    canvasGroup.blocksRaycasts = true;
                    canvasGroup.interactable = true;
                };
            }

            _saveLoadService.SaveProgress();
        }
    }
}
