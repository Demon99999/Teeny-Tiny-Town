using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.Services.StaticDataServices.Configs.Store;
using Assets.Scripts.UI.Screens.Map.Panels.Store;

namespace Assets.Scripts.GameLogic.Mover.Comand
{
    public class BuyGainStoreItemCommand : Command
    {
        private readonly GainStoreItemType _type;
        private readonly uint _price;
        private readonly GainBuyer _gainBuyer;
        private readonly GainStoreItemData _gainData;

        public BuyGainStoreItemCommand(
            IWorldChanger world,
            IMapData worldData,
            GainStoreItemType type,
            uint price,
            GainBuyer gainBuyer,
            GainStoreItemData gainData,
            NextBuildingForPlacingCreator nextBuildingForPlacingCreator)
            : base(world, worldData, nextBuildingForPlacingCreator)
        {
            _type = type;
            _price = price;
            _gainBuyer = gainBuyer;
            _gainData = gainData;
        }

        public override void Execute()
        {
            _gainData.ChangeBuyingCount(1);
            _gainBuyer.Buy(_type, _price);
        }

        public override void Undo()
        {
            _gainData.RevertBuyingCount(1);
            _gainBuyer.Undo(_type, _price);
            base.Undo();
        }
    }
}
