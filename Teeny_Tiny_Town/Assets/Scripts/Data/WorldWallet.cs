using System;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class WorldWallet : Wallet
    {
        public uint StartValue;

        public WorldWallet(uint startValue)
            : base(startValue) => StartValue = startValue;

        public void ForceGet(uint value)
        {
            if (value > Value)
            {
                Value = 0;

                return;
            }

            Value -= value;

            OnValueChanged();
        }

        public void Clear()
        {
            Value = StartValue;
            OnValueChanged();
        }
    }
}