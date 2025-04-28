using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using Assets.Scripts.Services.StaticDataServices.Configs.Store;

namespace Assets.Scripts.GameLogic.Mover
{
    public interface ICurrencyGameplayMover : IGameplayMover
    {
        void ChangeBuildingForPlacing(BuildingType targetBuildingType, uint buildingPrice);
        void BuyGainStoreItem(GainStoreItemType type, uint price);
    }
}