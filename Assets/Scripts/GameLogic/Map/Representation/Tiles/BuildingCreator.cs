using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic.Map.Representation.Tiles
{
    public class BuildingCreator : MonoBehaviour
    {
        [SerializeField] private GroundCreator _groundCreator;

        private IMapFactory _mapFactory;

        private Transform BuildingPoint => _groundCreator.Ground.BuildingPoint;

        [Inject]
        private void Construct(IMapFactory mapFactory)
        {
            _mapFactory = mapFactory;
        }

        public BuildingRepresentation Create(BuildingType buildingType)
        {
            return _mapFactory.CreateBuilding(buildingType, BuildingPoint.position, transform);
        }
    }
}