using System;
using System.Collections.Generic;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Map.Infrastructure
{
    public interface IWorldChanger : IBuildingGivable, ITileGetable
    {
        event Action TilesChanged;
        event Action<BuildingType> BuildingPlaced;
        event Action<Vector2Int, bool> CenterChanged;

        List<Tile> Tiles { get; }

        void Generate(ITileRepresentationCreatable tileRepresentationCreatable);
        void PlaceNewBuilding(Vector2Int gridPosition, BuildingType buildingType);
        void RemoveBuilding(Vector2Int destroyBuildingGridPosition);
        void ReplaceBuilding(Vector2Int fromBuildingGridPosition, BuildingType fromBuildingType, Vector2Int toBuildingGridPosition, BuildingType toBuildingType);
        void Start();
        void Update(bool isAnimate);
    }
}