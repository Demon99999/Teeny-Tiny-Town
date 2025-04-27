using Assets.Scripts.GameLogic.Map;
using Assets.Scripts.GameLogic.Map.Representation.Markers;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using Assets.Scripts.GameLogic.SandBox;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Factories
{
    public interface IMapFactory
    {
        WorldGenerator WorldGenerator { get; }

        WorldGenerator CreateWorldGenerator(Transform parent = null);
        TileRepresentation CreateTileRepresentation(Vector3 worldPosition, Transform transform);
        Ground CreateGround(TileType tileType, Vector3 position, Transform transform);
        Ground CreateGround(GroundType groundType, RoadType roadType, Vector3 position, GroundRotation rotation, Transform transform);
        void CreateCollectionItemCreator();
        BuildingRepresentation CreateBuilding(BuildingType buildingType, Vector3 position, Transform transform);
        SelectFrame CreateSelectFrame(Transform transform);
        void CreateBuildingMarker(Transform transform);
        SandboxWorld CreateSandboxWorld();
    }
}