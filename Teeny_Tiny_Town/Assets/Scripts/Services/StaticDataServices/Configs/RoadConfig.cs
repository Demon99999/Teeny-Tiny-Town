using System;
using System.Linq;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs
{
    [Serializable]
    public class RoadConfig
    {
        public RoadType Type;
        public Ground Prefab;
        public ValidNeighborsGridPositions[] ValidNeighborsGridPositions;

        public bool CheckSuitableNeighborsGridPositions(Vector2Int[] normalizedNeighborsGridPositions, out GroundRotation groundRotation)
        {
            groundRotation = default;

            foreach (ValidNeighborsGridPositions neighborGridPositions in ValidNeighborsGridPositions)
            {
                if (normalizedNeighborsGridPositions.Length == neighborGridPositions.NormalizedGridPositions.Length && neighborGridPositions.NormalizedGridPositions.Except(normalizedNeighborsGridPositions).Count() == 0)
                {
                    groundRotation = neighborGridPositions.Rotation;

                    return true;
                }
            }

            return false;
        }
    }
}