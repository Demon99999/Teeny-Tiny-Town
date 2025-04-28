using Assets.Scripts.Data.Map;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs.AdditionalBonuses
{
    [CreateAssetMenu(fileName = "ReplaceItemAdditionalBonusConfig", menuName = "StaticData/AdditionalBonus/Create new replace item additional bonus config", order = 51)]
    public class ReplaceItemAdditionalBonusConfig : AdditionalBonusConfig
    {
        public override AdditionalBonusType Type => AdditionalBonusType.ReplaceItem;

        public override void ApplyBonus(IMapData mapData)
        {
            mapData.ReplaceItems.AddItems(Count);
        }
    }
}