using Assets.Scripts.Data.Map;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs.AdditionalBonuses
{
    [CreateAssetMenu(fileName = "BuldozerAdditionalBonusConfig", menuName = "StaticData/AdditionalBonus/Create new buldozer additional bonus config", order = 51)]
    public class BuldozerAdditionalBonusConfig : AdditionalBonusConfig
    {
        public override AdditionalBonusType Type => AdditionalBonusType.Buldozer;

        public override void ApplyBonus(IMapData mapData)
        {
            mapData.BulldozerItems.AddItems(Count);
        }
    }
}