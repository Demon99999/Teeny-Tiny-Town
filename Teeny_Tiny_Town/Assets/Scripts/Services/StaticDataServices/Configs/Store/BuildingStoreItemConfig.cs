using System;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs.Store
{
    [Serializable]
    public class BuildingStoreItemConfig
    {
        [SerializeField] private BuildingType _buildingType;
        [SerializeField] private Sprite _iconAsset;
        [SerializeField] private uint _cost;
        [SerializeField] private float _costCoefficient;

        public BuildingType BuildingType => _buildingType;

        public Sprite IconAsset => _iconAsset;

        public uint Cost => _cost;

        public float CostCoefficient => _costCoefficient;

        public uint GetCost(uint n)
        {
            return (uint)(Cost * Mathf.Pow(CostCoefficient, n - 1));
        }

        public uint GetCostsSum(uint b, uint n)
        {
            uint sum = 0;

            for (uint i = b + 1; i <= n; i++)
                sum += GetCost(i);

            return sum;
        }
    }
}
