using System;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic.Map
{
    public class Map : MonoBehaviour, IMapRotation
    {
        private const int FullRotation = 360;
        private const int SimpleRotation = 90;

        private AnimationsConfig _animationsConfig;

        private Sequence _aroundRotating;
        private Quaternion _startRotation;
        private Tween _rotation;
        private Tween _movement;

        public event Action Entered;
        public event Action Cleaned;

        public float RotationDegrees { get; private set; }
        public bool IsCreated { get; private set; }

        [Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            _animationsConfig = staticDataService.AnimationsConfig;

            RotationDegrees = 0;
            IsCreated = false;
        }

        private void Start()
        {
            _startRotation = transform.rotation;
        }

        private void OnDestroy()
        {
            TryStopRotating();

            _movement?.Kill();
            _rotation?.Kill();
        }

        public void Enter()
        {
            Entered?.Invoke();
        }

        public void OnCreated()
        {
            IsCreated = true;
        }

        public void StartRotating()
        {
            _aroundRotating = DOTween
                .Sequence()
                .Append(transform.DORotate(new Vector3(transform.rotation.x, transform.rotation.y + FullRotation / 2, transform.rotation.z), _animationsConfig.WorldRotateDuration / 2).From(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z)).SetEase(Ease.Linear))
                .Append(transform.DORotate(new Vector3(transform.rotation.x, transform.rotation.y + FullRotation, transform.rotation.z), _animationsConfig.WorldRotateDuration / 2).SetEase(Ease.Linear))
                .SetLoops(-1, LoopType.Restart);
        }

        public void TryStopRotating()
        {
            _aroundRotating?.Kill();
        }

        public void RotateToStart(TweenCallback callback)
        {
            transform.DORotateQuaternion(_startRotation, _animationsConfig.WorldRotateToStarDuration).onComplete += callback;
        }

        public void Rotate�lockwise()
        {
            Rotate(SimpleRotation);
        }

        public void Rotate�ounterclockwise()
        {
            Rotate(-SimpleRotation);
        }

        public void MoveTo(Vector3 targetPosition, TweenCallback callback = null)
        {
            _movement = transform.DOMove(targetPosition, _animationsConfig.WorldMoveDuration);
            _movement.onComplete = callback;
        }

        public void Clean()
        {
            Cleaned?.Invoke();
        }

        private void Rotate(int degrees)
        {
            RotationDegrees += degrees;

            _rotation?.Kill();

            _rotation = transform.DORotateQuaternion(Quaternion.Euler(transform.rotation.x, RotationDegrees, transform.rotation.z), _animationsConfig.WorldSimpleRotateDuration);
        }
    }
}
