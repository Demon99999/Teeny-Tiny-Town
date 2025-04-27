using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Mover
{
    public interface IGameplayMover
    {
        void PlaceNewBuilding(Vector2Int gridPosition, BuildingType type);
        void RemoveBuilding(Vector2Int gridPosition);
        void ReplaceBuilding(Vector2Int fromGridPosition, BuildingType fromBuildingType, Vector2Int toGridPosition, BuildingType toBuildingType);
        void TryUndoCommand();
    }
}