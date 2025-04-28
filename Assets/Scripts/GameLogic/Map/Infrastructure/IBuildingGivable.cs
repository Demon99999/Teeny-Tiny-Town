using Assets.Scripts.GameLogic.Map.Infrastructure.Buildings;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Map.Infrastructure
{
    public interface IBuildingGivable
    {
        Building GetBuilding(BuildingType type, Vector2Int gridPosition);
    }
}