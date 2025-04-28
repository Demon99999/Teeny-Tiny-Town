using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.UI.Screens.ScreenStart.StorePanel
{
    public class StorePanel : BluredStartPanel
    {
        [SerializeField] private PackagesPanel _packagesPanel;

        public override void Open()
        {
            base.Open();
            _packagesPanel.Open();
        }

        protected override void Hide(TweenCallback callback)
        {
            base.Hide(callback);
            _packagesPanel.Hide();
        }
    }
}