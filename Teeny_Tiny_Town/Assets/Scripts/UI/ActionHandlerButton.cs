using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs;
using DG.Tweening;
using MPUIKIT;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI
{
    public class ActionHandlerButton : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private MPImage _background;

        private AnimationsConfig _animationsConfig;

        private Tween _tween;

        [Inject]
        private void Construct(IStaticDataService staticDataService) =>
            _animationsConfig = staticDataService.AnimationsConfig;

        private void OnDestroy() =>
            _tween?.Kill();

        protected void SetActive(bool isActive)
        {
            _tween?.Kill();

            Color targetColor = isActive ? _animationsConfig.ActiveGainButtonColor : _animationsConfig.DefaultGainButtonColor;

            _icon.color = isActive ? _animationsConfig.ActiveActionHandlerButtonIconColor : _animationsConfig.DefaultActionHandlerButtonIconColor;
            _tween = _background.DOColor(targetColor, _animationsConfig.ChangeGainButtonActiveDuration);
        }
    }
}
