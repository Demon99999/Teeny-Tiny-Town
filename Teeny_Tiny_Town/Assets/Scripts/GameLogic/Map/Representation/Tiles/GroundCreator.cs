using System.Collections;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic.Map.Representation.Tiles
{
    public class GroundCreator : MonoBehaviour
    {
        [SerializeField] private Transform _groundPoint;

        private IMapFactory _mapFactory;
        private AnimationsConfig _animationsConfig;

        public Ground Ground { get; private set; }

        [Inject]
        private void Construct(IMapFactory mapFactory, IStaticDataService staticDataService)
        {
            _mapFactory = mapFactory;
            _animationsConfig = staticDataService.AnimationsConfig;
        }

        public void Create(TileType tileType)
        {
            if (Ground != null)
            {
                Destroy(Ground.gameObject);
            }

            Ground = _mapFactory.CreateGround(tileType, _groundPoint.position, transform);
        }

        public void Create(GroundType groundType, RoadType roadType, GroundRotation rotation, bool isAnimate)
        {
            if (Ground != null && Ground.gameObject != null)
            {
                Destroy(Ground.gameObject);
            }

            if (_groundPoint == null)
            {
                return;
            }

            Ground = _mapFactory.CreateGround(groundType, roadType, _groundPoint.position, rotation, transform);

            if (isAnimate)
            {
                StartCoroutine(WaitForAnimation());
            }
        }

        private IEnumerator WaitForAnimation()
        {
            yield return new WaitForSeconds(_animationsConfig.TileUpdatingDuration);
        }
    }
}