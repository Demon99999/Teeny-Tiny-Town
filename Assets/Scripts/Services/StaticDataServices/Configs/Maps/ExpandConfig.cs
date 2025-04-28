using System;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs.Maps
{
    [Serializable]
    public class ExpandConfig
    {
        public BuildingType BuidldingType;
        public Vector2Int ExpandedSize;
    }
}