using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs.GameStore
{
    public abstract class StoreItemConfig : ScriptableObject
    {
        [SerializeField] private GameplayStoreItemType _type;
        [SerializeField] private uint _cost;

        public GameplayStoreItemType Type => _type;

        public uint Cost => _cost;

        public abstract void Unlock(StoreData storeData);
        public abstract bool NeedToShow(StoreData storeData);
    }
}
