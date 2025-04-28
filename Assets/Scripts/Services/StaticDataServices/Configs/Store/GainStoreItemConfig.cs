using Assets.Scripts.UI.Screens.Map.Panels.Store.GainPanels;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs.Store
{
    [CreateAssetMenu(fileName = "GainStorItemConfig", menuName = "StaticData/WorldStore/Create new gain store item config", order = 51)]
    public class GainStoreItemConfig : ScriptableObject
    {
        [SerializeField] private GainStoreItemType _type;
        [SerializeField] private GainStoreItemPanel _panelAssetReference;
        [SerializeField] private Sprite _spriteconAssetReference;
        [SerializeField] private string _name;
        [SerializeField] private uint _cost;
        [SerializeField] private float _costCoefficient;

        public GainStoreItemType Type => _type;

        public GainStoreItemPanel PanelAssetReference => _panelAssetReference;

        public Sprite SpriteconAssetReference => _spriteconAssetReference;

        public string Name => _name;

        public uint Cost => _cost;

        public float CostCoefficien => _costCoefficient;

        public virtual GainStoreItemData GetData()
        {
            return new GainStoreItemData(Type);
        }

        public uint GetCost(uint value)
        {
            uint cost = Cost;
            float coeff = CostCoefficien;

            uint valueCost = (uint)(cost * Mathf.Pow(coeff, value - 1));

            return valueCost;
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
