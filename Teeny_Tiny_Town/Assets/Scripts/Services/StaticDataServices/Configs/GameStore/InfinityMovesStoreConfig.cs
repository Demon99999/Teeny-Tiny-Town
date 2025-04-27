using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs.GameStore
{
    [CreateAssetMenu(fileName = "InfinityMovesStoreConfig", menuName = "StaticData/GameplayStore/Create new infinity moves store config", order = 51)]
    public class InfinityMovesStoreConfig : StoreItemConfig
    {
        public override bool NeedToShow(StoreData storeData)
        {
            return storeData.IsInventoryUnlocked == false;
        }

        public override void Unlock(StoreData storeData)
        {
            storeData.IsInfinityMovesUnlocked = true;
        }
    }
}
