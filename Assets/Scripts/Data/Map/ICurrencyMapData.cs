namespace Assets.Scripts.Data.Map
{
    public interface ICurrencyMapData : IMapData
    {
        WorldWallet WorldWallet { get; }
        WorldMovesCounterData MovesCounter { get; }
        WorldStore WorldStore { get; }

        void AddBuildingIncome(uint amount);
        void AddLighthouseBonus(uint amount);
        void RemoveBuildingIncome(uint amount);
        void RemoveLighthouseBonus(uint amount);
        int GetTotalIncome();
    }
}