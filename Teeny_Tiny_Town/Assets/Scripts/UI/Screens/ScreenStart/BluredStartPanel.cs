using DG.Tweening;
using Zenject;

namespace Assets.Scripts.UI.Screens.ScreenStart
{
    public class BluredStartPanel : StartWindowPanel
    {
        private Blur _blur;

        [Inject]
        private void Construct(Blur blur)
        {
            _blur = blur;
        }

        public override void Open()
        {
            base.Open();
            _blur.Open();
        }

        protected override void Hide(TweenCallback callback)
        {
            base.Hide(callback);
            _blur.Hide();
        }
    }
}
