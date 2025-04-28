using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.StateMachineMap;
using Assets.Scripts.Services.Input;
using Assets.Scripts.Services.StaticDataServices.Configs.Store;

namespace Assets.Scripts.GameLogic.Map.Representation.ActionHandler
{
    public class CurrencyWorldActionHandlerSwitcher : ActionHandlerSwitcher
    {
        private readonly WorldStateMachine _worldStateMachine;

        public CurrencyWorldActionHandlerSwitcher(
            ActionHandlerStateMachine handlerStateMachine,
            WorldRepresentationChanger worldRepresentationChanger,
            IInputService inputService,
            IMapData mapData,
            WorldStateMachine worldStateMachine)
            : base(handlerStateMachine, worldRepresentationChanger, inputService, mapData)
        {
            _worldStateMachine = worldStateMachine;
        }

        protected override bool CheckBulldozerItemsCount()
        {
            bool isEnoughItems = base.CheckBulldozerItemsCount();

            if (isEnoughItems == false)
            {
                _worldStateMachine.Enter<GainBuyingState, GainStoreItemType>(GainStoreItemType.Bulldozer);
            }

            return isEnoughItems;
        }

        protected override bool CheckReplaceItemsCount()
        {
            bool isEnoughItems = base.CheckReplaceItemsCount();

            if (isEnoughItems == false)
            {
                _worldStateMachine.Enter<GainBuyingState, GainStoreItemType>(GainStoreItemType.ReplaceItems);
            }

            return isEnoughItems;
        }
    }
}
