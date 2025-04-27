using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.GameLogic.Map.StateMachineMap;
using Assets.Scripts.GameLogic.Mover.Comand;
using Assets.Scripts.Services.Input;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using Assets.Scripts.Services.StaticDataServices.Configs.Store;
using Assets.Scripts.UI.Screens.Map.Panels.Store;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Mover
{
    public class CurrencyGameplayMover : GameplayMover, ICurrencyGameplayMover
    {
        private readonly ICurrencyMapData _currencyMapData;
        private readonly WorldStateMachine _worldStateMachine;

        public CurrencyGameplayMover(
            IWorldChanger worldChanger,
            IInputService inputService,
            ICurrencyMapData mapData,
            IPersistantProgrss persistentProgressService,
            WorldStateMachine worldStateMachine,
            NextBuildingForPlacingCreator nextBuildingForPlacingCreator)
            : base(worldChanger, inputService, mapData, persistentProgressService, nextBuildingForPlacingCreator)
        {
            _currencyMapData = mapData;
            _worldStateMachine = worldStateMachine;
        }

        public override void OpenChest(Vector2Int chestGridPosition, uint reward)
        {
            ExecuteCommand(new OpenChestCommand(WorldChanger, MapData, reward, chestGridPosition, _currencyMapData.WorldWallet, NextBuildingForPlacingCreator));
        }

        public void ChangeBuildingForPlacing(BuildingType targetBuildingType, uint buildingPrice)
        {
            ExecuteCommand(new ChangeBuildingForPlacingCommand(WorldChanger, MapData, targetBuildingType, buildingPrice, _currencyMapData.WorldWallet, NextBuildingForPlacingCreator));
        }

        public void BuyGainStoreItem(GainStoreItemType type, uint price)
        {
            var gainData = _currencyMapData.WorldStore.GetGainData(type);
            ExecuteCommand(new BuyGainStoreItemCommand(
                WorldChanger,
                MapData,
                type,
                price,
                new GainBuyer(
                    _worldStateMachine,
                    NextBuildingForPlacingCreator,
                    this,
                    _currencyMapData),
                gainData,
                NextBuildingForPlacingCreator));
        }

        protected override void ExecuteCommand(Command command)
        {
            base.ExecuteCommand(command);
            _currencyMapData.MovesCounter.Move();
        }
    }
}
