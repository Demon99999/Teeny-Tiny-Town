using System;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class StoreData
    {
        public bool IsInventoryUnlocked;
        public bool IsInfinityMovesUnlocked;

        public StoreData()
        {
            IsInventoryUnlocked = false;
            IsInfinityMovesUnlocked = false;
        }
    }
}