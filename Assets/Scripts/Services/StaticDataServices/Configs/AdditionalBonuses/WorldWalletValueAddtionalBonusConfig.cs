using Assets.Scripts.Data.Map;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs.AdditionalBonuses
{
    [CreateAssetMenu(fileName = "WorldWalletValueAddtionalBonusConfig", menuName = "StaticData/AdditionalBonus/Create new world wallet value additional bonus config", order = 51)]
    public class WorldWalletValueAddtionalBonusConfig : AdditionalBonusConfig
    {
        public override AdditionalBonusType Type => AdditionalBonusType.WorldWalletValue;

        public override void ApplyBonus(IMapData worldData)
        {
            if (worldData is ICurrencyMapData currencyWorldData)
            {
                currencyWorldData.WorldWallet.Give(Count);
            }
        }
    }
}