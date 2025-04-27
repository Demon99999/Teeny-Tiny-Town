using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs.GameStore
{
    [CreateAssetMenu(fileName = "InventorStoreConfig", menuName = "StaticData/GameplayStore/Create new inventory store config", order = 51)]
    public class InventoryStoreConfig : StoreItemConfig
    {
        public override bool NeedToShow(StoreData storeData)
        {
            return storeData.IsInventoryUnlocked == false;
        }

        public override void Unlock(StoreData storeData)
        {
            storeData.IsInventoryUnlocked = true;
        }
    }
}
