using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.UI.Screens.Education
{
    public class ShowUiPanel : PlacedBuildingEducationPanel
    {
        [SerializeField] private CanvasGroup[] _showedUi;

        public override void Open()
        {
            base.Open();

            foreach (CanvasGroup canvasGroup in _showedUi)
            {
                canvasGroup.DOFade(1, AnimationsConfig.WindowOpeningStateDuration).onComplete += () =>
                {
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                };
            }
        }
    }
}