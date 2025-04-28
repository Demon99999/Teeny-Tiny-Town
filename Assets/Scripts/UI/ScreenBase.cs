using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
    public class ScreenBase : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private Tween _fader;

        protected AnimationsConfig AnimationsConfig { get; private set; }

        [Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            AnimationsConfig = staticDataService.AnimationsConfig;
        }

        private void OnDestroy()
        {
            _fader?.Kill();
        }

        public virtual void Open()
        {
            Fade(1, callback: () =>
            {
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
            });
        }

        public virtual void Hide(TweenCallback callback)
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            Fade(0, callback);
        }

        public void HideImmediately()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void Fade(int targetValue, TweenCallback callback)
        {
            _fader?.Kill();
            _fader = _canvasGroup.DOFade(targetValue, 0.2f);
            _fader.onComplete += callback;
        }
    }
}
