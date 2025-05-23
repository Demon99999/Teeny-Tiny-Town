﻿using Assets.Scripts.Camera;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI
{
    public class Blur : ScreenBase
    {
        private const string BlurMaterial = "_Blur";
        private const float MaxBlur = 0.017f;
        private const float MinBlur = 0;
        private const int PlaneDistance = 1;

        [SerializeField] private Image _blur;
        [SerializeField] private Canvas _canvas;

        [Inject]
        private void Construct(GameplayCamera gameplayCamera)
        {
            _canvas.worldCamera = gameplayCamera.MainCamera;
            _canvas.planeDistance = PlaneDistance;
        }

        public override void Open()
        {
            base.Open();
            ChangeBlured(MaxBlur);
        }

        public override void Hide(TweenCallback callback = null)
        {
            base.Hide(callback);
            ChangeBlured(MinBlur);
        }

        private void ChangeBlured(float targetValue)
        {
            _blur.material.DOFloat(targetValue, BlurMaterial, AnimationsConfig.WindowOpeningStateDuration);
        }

        public class BlurFactory : PlaceholderFactory<Blur>
        {

        }
    }
}
