using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.UI.Screens.ScreenStart
{
    public abstract class CanvasGroupPanel : Panel
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        public CanvasGroup CanvasGroup => _canvasGroup;

        public override abstract void Open();

        protected void ChangeCanvasGroupAlpha(float targetValue, TweenCallback callback = null)
        { 
            _canvasGroup.DOFade(targetValue, AnimationsConfig.WindowOpeningStateDuration).onComplete += callback;
        }
    }
}