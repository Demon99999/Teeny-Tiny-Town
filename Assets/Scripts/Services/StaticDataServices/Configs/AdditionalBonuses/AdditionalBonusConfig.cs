using Assets.Scripts.Data.Map;
using Assets.Scripts.UI.Screens.Map.Panels.AdditionalBonusOffer;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs.AdditionalBonuses
{
    public abstract class AdditionalBonusConfig : ScriptableObject
    {
        [SerializeField] private AdditionalBonusOfferItem _panelAssetReference;
        [SerializeField] private Sprite _iconAssetReference;
        [SerializeField] private uint _count;
        [SerializeField] private uint _cost;

        public AdditionalBonusOfferItem PanelAssetReference => _panelAssetReference;

        public Sprite IconAssetReference => _iconAssetReference;

        public uint Count => _count;

        public uint Cost => _cost;

        public abstract AdditionalBonusType Type { get; }

        public abstract void ApplyBonus(IMapData mapData);
    }
}