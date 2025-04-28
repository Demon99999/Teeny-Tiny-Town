using System;
using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure.Buildings;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Map.Infrastructure
{
    public class Tile
    {
        public readonly TileType Type;
        public readonly Vector2Int GridPosition;
        protected readonly TileData TileData;
        protected readonly IStaticDataService StaticDataService;

        public Tile(TileData tileData, TileType type, IStaticDataService staticDataService, Building building)
        {
            TileData = tileData;
            Type = type;
            StaticDataService = staticDataService;
            Building = building;
            GridPosition = TileData.GridPosition;
        }

        public Building Building { get; protected set; }
        public bool IsEmpty => Building == null;
        public BuildingType BuildingType => IsEmpty ? BuildingType.Undefined : Building.Type;
        protected TileRepresentation TileRepresentation { get; private set; }

        public void CreateRepresentation(ITileRepresentationCreatable tileRepresentationCreatable)
        {
            TileRepresentation = tileRepresentationCreatable.Create(GridPosition, Type);
            CreateGroundRepresentation(false);

            if (Building != null)
            {
                Building.CreateRepresentation(TileRepresentation, false, false);
            }
        }

        public virtual void UpdateBuilding(Building building, IBuildingsUpdatable buildingsUpdatable, bool isAnimate)
        {
            if (building == null)
            {
                return;
            }

            SetUpBuilding(building);
        }

        public void PutBuilding(Building building)
        {
            if (building == null)
            {
                RemoveBuilding();

                return;
            }

            SetUpBuilding(building);
        }

        public void Destroy()
        {
            TileRepresentation.Destroy();
        }

        public virtual void CleanAll(bool isAnimate)
        {
            if (IsEmpty == false)
            {
                if (isAnimate)
                {
                    TileRepresentation.AnimateDestroyBuilding();
                }
                else
                {
                    TileRepresentation.DestroyBuilding(false);
                }
            }

            SetBuilding(null);
        }

        public void RemoveBuilding()
        {
            if (IsEmpty)
            {
                return;
            }

            TileRepresentation.DestroyBuilding(true);
            Clean();
        }

        public void RemoveBuilding(Vector3 destroyPosition)
        {
            if (IsEmpty)
            {
                return;
            }

            TileRepresentation.DestroyBuilding(destroyPosition);
            Clean();
        }

        public void DisposeBuilding()
        {
            if (Building != null && Building is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        protected virtual void Clean()
        {
            SetBuilding(null);
        }

        protected virtual void SetUpBuilding(Building building)
        {
            CreateBuildingRepresentation(building);
        }

        protected virtual void CreateGroundRepresentation(bool isWaitedForCreation)
        {
            TileRepresentation.GroundCreator.Create(Type);
        }

        protected virtual void CreateBuildingRepresentation(Building building)
        {
            SetBuilding(building);

            Building.CreateRepresentation(TileRepresentation, true, true);
        }

        protected void SetBuilding(Building building)
        {
            if (Building != building)
            {
                DisposeBuilding();
            }

            Building = building;
            TileData.BuildingType = IsEmpty ? BuildingType.Undefined : Building.Type;
        }
    }
}