using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic.Map.Representation
{
    public class WorldPositioner : MonoBehaviour
    {
        [SerializeField] private WorldGenerator _worldGenerator;

        private ICenterChangeable _centerChangeable;
        private AnimationsConfig _animationsConfig;

        [Inject]
        private void Construct(ICenterChangeable centerChangeable, IStaticDataService staticDataService)
        {
            _centerChangeable = centerChangeable;
            _animationsConfig = staticDataService.AnimationsConfig;

            _centerChangeable.CenterChanged += OnCenterChanged;
        }

        private void OnDestroy()
        {
            _centerChangeable.CenterChanged -= OnCenterChanged;
        }

        private void OnCenterChanged(Vector2Int size, bool isNeedAnimate)
        {
            if (isNeedAnimate)
            {
                AnimatePlaceToCenter(size);
            }
            else
            {
                PlaceToCenter(size);
            }
        }

        private void PlaceToCenter(Vector2Int size)
        {
            Vector3 center = GetCenter(size);

            transform.localPosition = -center;
        }

        private void AnimatePlaceToCenter(Vector2Int size)
        {
            Vector3 center = GetCenter(size);

            transform.DOLocalMove(-center, _animationsConfig.WorldMoveToCenterDuration);
        }

        private Vector3 GetCenter(Vector2Int size)
        {
            return new Vector3(
                (size.x * _worldGenerator.CellSize / 2f) - (_worldGenerator.CellSize / 2f),
                transform.position.y,
                (size.y * _worldGenerator.CellSize / 2f) - (_worldGenerator.CellSize / 2f));
        }
    }
}