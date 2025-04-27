using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs
{
    [CreateAssetMenu(fileName = "GroundsConfig", menuName = "StaticData/Create new grounds config", order = 51)]
    public class GroundsConfig : ScriptableObject
    {
        public GroundConfig[] GroundConfigs;
        public GroundDefaultSurfaceConfig[] DefaultGroundConfigs;

        public RoadType GetRoadType(Vector2Int gridPosition, List<Vector2Int> adjacentGridPositions, GroundType groundType, out GroundRotation rotation)
        {
            GroundConfig groundConfig = GroundConfigs.First(value => value.Type == groundType);

            return groundConfig.GetRoadType(gridPosition, adjacentGridPositions, out rotation);
        }
    }
}
