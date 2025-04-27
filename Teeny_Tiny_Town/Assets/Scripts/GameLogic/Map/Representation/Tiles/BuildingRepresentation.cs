using System.Collections;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic.Map.Representation.Tiles
{
    public class BuildingRepresentation : MonoBehaviour
    {
        private AnimationsConfig _animationsConfig;
        private Sequence _sequence;
        private Vector3 _startPosition;

        public BuildingType Type { get; private set; }

        [Inject]
        private void Construct(IStaticDataService staticDataservice)
        {
            _animationsConfig = staticDataservice.AnimationsConfig;
        }

        private void OnDestroy()
        {
            DOTween.Kill(transform);
            
            if (_sequence != null)
            {
                _sequence.Kill();
                _sequence = null;
            }
        }


        public void Init(BuildingType type)
        {
            Type = type;
        }

        public void StopShaking()
        {
            if (TryKillSequence())
                transform.position = _startPosition;
        }

        public void StopBlinking()
        {
            TryKillSequence();

            transform.localScale = Vector3.one;
        }

        public void Shake()
        {
            TryKillSequence();
            _startPosition = transform.position;

            if (!gameObject.activeSelf) return;

            _sequence = DOTween
                .Sequence()
                .Append(transform.DOMove(_startPosition + _animationsConfig.BuildingShakeOffset, _animationsConfig.BuildingShakeTweenDuration).SetEase(_animationsConfig.BuildingShakeCurve))
                .SetLoops(_animationsConfig.BuildingShakesCount, LoopType.Restart).OnKill(() => _sequence = null).SetLink(gameObject);
        }

        public void Blink()
        {
            TryKillSequence();

            _sequence = DOTween
                .Sequence()
                .Append(transform.DOScale(_animationsConfig.BuildingBlinkingScale, _animationsConfig.BuildingBlinkingDuration).SetEase(Ease.OutSine))
                .Append(transform.DOScale(1, _animationsConfig.BuildingBlinkingDuration).SetEase(Ease.OutSine))
                .SetLoops(-1).OnKill(() => _sequence = null).SetLink(gameObject);
        }

        public void AnimateDestroy(Vector3 destroyPosition)
        {
            if (!this || !gameObject) return;

            transform.DOJump(
                    destroyPosition + _animationsConfig.BuildingJumpDestroyOffset,
                    _animationsConfig.BuildingJumpDestroyPower,
                    1,
                    _animationsConfig.BuildingJumpDestroyDuration)
                .SetLink(gameObject)
                .OnComplete(() =>
                {
                    if (this != null && gameObject != null)
                        Destroy();
                });
        }

        public void Destroy()
        {
            DOTween.Kill(transform);

            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }

        public void AnimateDestroy()
        {
            transform.DOScale(0, _animationsConfig.TileUpdatingDuration)
                .SetLink(gameObject)
                .OnComplete(Destroy);
        }

        public void AnimatePut(bool waitForCompletion)
        {
            StartCoroutine(AnimatePutRoutine(waitForCompletion));
        }

        public IEnumerator AnimatePutRoutine(bool waitForCompletion)
        {
            if (!gameObject.activeSelf) yield break;

            float tweenDuration = _animationsConfig.BuildingPutDuration / 3;

            Sequence sequence = DOTween
                .Sequence()
                .Append(transform.DOScale(_animationsConfig.BuildingPutMaxScale, tweenDuration))
                .Append(transform.DOScale(_animationsConfig.BuildingPutMinScale, tweenDuration))
                .Append(transform.DOScale(1f, tweenDuration));

            if (waitForCompletion)
            {
                yield return sequence.WaitForCompletion();
            }
            else
            {
                yield return new WaitForSeconds(_animationsConfig.TileUpdatingDuration);
            }
        }

        private bool TryKillSequence()
        {
            if (_sequence != null)
            {
                _sequence.Kill();
                _sequence = null;
                return true;
            }

            return false;
        }
    }
}