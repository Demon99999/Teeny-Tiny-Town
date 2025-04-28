using System;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class WorldMovesCounterData
    {
        private const uint MovesCountToBuildingsPayment = 5;

        public uint MovesCount;

        public event Action TimeToPaymentPayableBuildings;

        public void Move()
        {
            MovesCount++;

            if (MovesCount % MovesCountToBuildingsPayment == 0)
            {
                TimeToPaymentPayableBuildings?.Invoke();
            }
        }
    }
}