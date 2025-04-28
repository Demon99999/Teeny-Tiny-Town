using Assets.Scripts.GameLogic.Map.Representation.Markers;
using Assets.Scripts.GameLogic.Mover;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Map.Representation.ActionHandler
{
    public abstract class WorldActionHandlerState : ActionHandlerState
    {
        protected readonly IGameplayMover GameplayMover;

        protected WorldActionHandlerState(SelectFrame selectFrame, LayerMask layerMask, IGameplayMover gameplayMover)
            : base(selectFrame, layerMask)
        {
            GameplayMover = gameplayMover;
        }

        protected bool CheckBuildingAndTileCompatibility(BuildingType buildingType, TileType tileType)
        {
            return ((buildingType == BuildingType.Lighthouse && tileType != TileType.WaterSurface)
                || (buildingType != BuildingType.Lighthouse && tileType == TileType.WaterSurface))
                == false;
        }
    }
}
