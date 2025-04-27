using System;
using System.Collections.Generic;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs
{
    [Serializable]
    public class GroundConfig
    {
        public GroundType Type;
        public RoadConfig[] RoadConfigs;

        public RoadType GetRoadType(Vector2Int gridPosition, List<Vector2Int> adjacentGridPositions, out GroundRotation rotation)
        {
            rotation = default;

            Vector2Int[] normalizedNeighborsGridPositions = new Vector2Int[adjacentGridPositions.Count];

            for (int i = 0; i < normalizedNeighborsGridPositions.Length; i++)
                normalizedNeighborsGridPositions[i] = adjacentGridPositions[i] - gridPosition;

            foreach (RoadConfig roadConfig in RoadConfigs)
            {
                if (roadConfig.CheckSuitableNeighborsGridPositions(normalizedNeighborsGridPositions, out rotation))
                    return roadConfig.Type;
            }

            return default;
        }
    }
}