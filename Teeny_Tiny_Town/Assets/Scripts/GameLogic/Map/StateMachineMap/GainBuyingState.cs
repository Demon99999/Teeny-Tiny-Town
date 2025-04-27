using Assets.Scripts.Infrastructure.StateMachine;
using Assets.Scripts.Services.StaticDataServices.Configs.Store;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Screens.Map;
using Assets.Scripts.UI.Screens.Map.Panels.Store;

namespace Assets.Scripts.GameLogic.Map.StateMachineMap
{
    public class GainBuyingState : IPayLoadedState<GainStoreItemType>
    {
        private readonly UnlimitedQuantityGainBuyer _gainBuyer;
        private readonly ScreensSwitcher _screensSwitcher;

        public GainBuyingState(UnlimitedQuantityGainBuyer gainBuyer, ScreensSwitcher screensSwitcher)
        {
            _gainBuyer = gainBuyer;
            _screensSwitcher = screensSwitcher;
        }

        public void Enter(GainStoreItemType gainStoreItemType)
        {
            _gainBuyer.SetBuyingGainType(gainStoreItemType);
            _screensSwitcher.Switch<GainBuyingWindow>();
        }

        public void Exit() { }
    }
}
