using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.GameLogic.Map.StateMachineMap;
using Assets.Scripts.GameLogic.Mover;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using Assets.Scripts.Services.StaticDataServices.Configs.Store;

namespace Assets.Scripts.UI.Screens.Map.Panels.Store
{
    public class GainBuyer
    {
        private readonly WorldStateMachine _worldStateMachine;
        private readonly NextBuildingForPlacingCreator _buildingCreator;
        private readonly ICurrencyGameplayMover _gameplayMover;
        private readonly ICurrencyMapData _currencyMapData;
        private BuildingType? _previousBuildingType;

        public GainBuyer(
            WorldStateMachine worldStateMachine,
            NextBuildingForPlacingCreator buildingCreator,
            ICurrencyGameplayMover gameplayMover,
            ICurrencyMapData currencyMapData)
        {
            _worldStateMachine = worldStateMachine;
            _buildingCreator = buildingCreator;
            _gameplayMover = gameplayMover;
            _currencyMapData = currencyMapData;
        }

        public void Buy(GainStoreItemType type, uint price)
        {
            _previousBuildingType = _buildingCreator.BuildingsForPlacingData?.CurrentBuildingType;

            switch (type)
            {
                case GainStoreItemType.ReplaceItems:
                case GainStoreItemType.Bulldozer:
                    _worldStateMachine.Enter<GainBuyingState, GainStoreItemType>(type);
                    break;

                case GainStoreItemType.Crane:
                    _buildingCreator.ChangeCurrentBuildingForPlacing(BuildingType.Crane);
                    _currencyMapData.WorldWallet.TryGet(price);
                    break;

                case GainStoreItemType.Lighthouse:
                    _buildingCreator.ChangeCurrentBuildingForPlacing(BuildingType.Lighthouse);
                    _currencyMapData.WorldWallet.TryGet(price);
                    break;
            }
        }

        public void Undo(GainStoreItemType type, uint price)
        {
            switch (type)
            {
                case GainStoreItemType.Crane:
                case GainStoreItemType.Lighthouse:
                    _currencyMapData.WorldWallet.Give(price);

                    if (_previousBuildingType.HasValue)
                    {
                        _buildingCreator.ChangeCurrentBuildingForPlacing(_previousBuildingType.Value);
                    }

                    break;
            }
        }
    }
}