using UnityEngine;

namespace Assets.Scripts.UI.Screens.SelectionMap
{
    public class MapSelectionPanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        public virtual void Open()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }
    }
}