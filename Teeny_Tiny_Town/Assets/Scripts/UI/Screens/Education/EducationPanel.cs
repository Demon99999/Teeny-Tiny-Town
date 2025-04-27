using System;
using Assets.Scripts.UI.Screens.ScreenStart;
using DG.Tweening;

namespace Assets.Scripts.UI.Screens.Education
{
    public abstract class EducationPanel : CanvasGroupPanel
    {
        public event Action Handled;

        public override void Open()
        {
            ChangeCanvasGroupAlpha(1, callback: () =>
            {
                CanvasGroup.blocksRaycasts = true;
                CanvasGroup.interactable = true;
            });
        }

        public virtual void Hide(TweenCallback callback)
        {
            CanvasGroup.blocksRaycasts = false;
            CanvasGroup.interactable = false;
            ChangeCanvasGroupAlpha(0, callback);
        }

        protected void OnHandled()
        {
            Handled?.Invoke();
        }
    }
}
