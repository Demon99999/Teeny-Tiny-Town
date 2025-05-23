﻿using System;

namespace Assets.Scripts.Services.StaticDataServices.Configs.Store
{
    [Serializable]
    public class GainStoreItemData
    {
        public GainStoreItemType Type;
        public uint BuyingCount;
        public uint RemainingCount;
        public bool IsLimited;
        public uint StartRemainingCount;

        public GainStoreItemData(GainStoreItemType type)
        {
            Type = type;

            BuyingCount = 0;
            IsLimited = false;
        }

        public GainStoreItemData(GainStoreItemType type, uint startCount)
        {
            Type = type;
            RemainingCount = startCount;

            IsLimited = true;
            StartRemainingCount = startCount;
        }

        public event Action BuyingCountChanged;

        public virtual void ChangeBuyingCount(uint count)
        {
            RemainingCount = RemainingCount < count ? 0 : RemainingCount - count;
            BuyingCount += count;
            BuyingCountChanged?.Invoke();
        }

        public virtual void RevertBuyingCount(uint count)
        {
            if (IsLimited)
            {
                RemainingCount += count;
            }

            BuyingCount -= count;
            BuyingCountChanged?.Invoke();
        }

        public void Clear()
        {
            BuyingCount = 0;
            RemainingCount = StartRemainingCount;

            BuyingCountChanged?.Invoke();
        }
    }
}