using System;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs
{
    [Serializable]
    public class ValidNeighborsGridPositions
    {
        public GroundRotation Rotation;
        public Vector2Int[] NormalizedGridPositions;
    }
}