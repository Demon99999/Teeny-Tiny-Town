﻿using System;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class Wallet
    {
        public uint Value;

        public Wallet(uint startValue)
        {
            Value = startValue;
        }

        public event Action<uint> ValueChanged;

        public void Give(uint value)
        {
            Value += value;
            ValueChanged?.Invoke(Value);
        }

        public bool TryGet(uint value)
        {
            if (value > Value)
            {
                return false;
            }

            Value -= value;
            ValueChanged?.Invoke(Value);

            return true;
        }

        protected void OnValueChanged()
        {
            ValueChanged?.Invoke(Value);
        }
    }
}