using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic.Map.Representation.Tiles
{
    public class TileRepresentation : MonoBehaviour
    {
        [SerializeField] private GroundCreator _groundCreator;
        [SerializeField] private BuildingCreator _buildingCreator;
        [SerializeField] private AudioSource _putBuildingAudioSource;
        [SerializeField] private AudioSource _destroyBuildinAudioSource;
        [SerializeField] private AudioSource _cleanTileAudioSource;
        [SerializeField] private AudioSource _mergeBuildingAudioSource;

        private BuildingRepresentation _building;

        public TileType Type { get; private set; }
        public Vector2Int GridPosition { get; private set; }
        public bool IsEmpty => _building == null;
        public BuildingType BuildingType => IsEmpty ? BuildingType.Undefined : _building.Type;
        public Transform BuildingPoint => _groundCreator.Ground.BuildingPoint;
        public GroundCreator GroundCreator => _groundCreator;

        public void Init(TileType type, Vector2Int gridPosition)
        {
            Type = type;
            GridPosition = gridPosition;
        }

        public void ShakeBuilding()
        {
            _building.Shake();
        }

        public void StopBuildingShaking()
        {
            _building.StopShaking();
        }

        public void RaiseBuilding(Vector3 offset)
        {
            _building.transform.position += offset;
        }

        public void LowerBuilding()
        {
            if (_building != null)
            {
                PlaceBuildingToBuildingPoint(_building);
            }
        }

        public void DestroyBuilding(bool needSound)
        {
            if (_building != null)
            {
                if (needSound)
                {
                    _destroyBuildinAudioSource.Play();
                }

                _building.Destroy();
                _building = null;
            }
        }

        public void DestroyBuilding(Vector3 destroyPosition)
        {
            _mergeBuildingAudioSource.Play();
            _building.AnimateDestroy(destroyPosition);
            _building = null;
        }

        public void AnimateDestroyBuilding()
        {
            _cleanTileAudioSource.Play();
            _building.AnimateDestroy();
            _building = null;
        }

        public TBuilding TryChangeBuilding<TBuilding>(BuildingType targetBuildingType, bool isAnimate, bool waitForCompletion) where TBuilding : BuildingRepresentation
        {
            if (BuildingType != targetBuildingType)
            {
                if (IsEmpty == false)
                {
                    DestroyBuilding(false);
                }

                if (targetBuildingType != BuildingType.Undefined)
                {
                    _building =_buildingCreator.Create(targetBuildingType);

                    if (isAnimate)
                    {
                        _putBuildingAudioSource.Play();
                        _building.AnimatePut(waitForCompletion);
                    }

                    return _building as TBuilding;
                }
            }

            return null;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void PlaceBuildingToBuildingPoint(BuildingRepresentation building)
        {
            building.transform.position = _groundCreator.Ground.BuildingPoint.position;
        }

        public class Factory : PlaceholderFactory<TileRepresentation> { }
    }
}