using Assets.Scripts.Services.StaticDataServices.Configs.AdditionalBonuses;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using Assets.Scripts.Services.StaticDataServices.Configs.Reward;
using Assets.Scripts.Services.StaticDataServices.Configs.Screens;
using Assets.Scripts.Services.StaticDataServices.Configs.Store;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Screens.Map.Panels.Store;
using Assets.Scripts.UI.Screens.Map.Panels.Store.GainPanels;
using Assets.Scripts.UI.Screens.Map.Quest;
using Assets.Scripts.UI.Screens.Map.Reward;
using Assets.Scripts.UI.Screens.SandBox;
using Assets.Scripts.UI.Screens.SelectionMap;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Factories
{
    public interface IUIFactory
    {
        ScreenBase CreateScreen(ScreenType screenType);
        QuestPanel CreateQuestPanel(string id, Transform parent);
        RewardPanel CreateRewardPanel(RewardType rewardType, Transform transform);
        BuildingStoreItem CreateStoreItem(BuildingType buildingType, Transform transform);
        SandboxPanelElement CreateSandboxPanelElement(Transform content, Sprite iconAssetReference);
        PeculiarityIconPanel CreatePeculiarityIconPanel(Sprite iconAssetReference, Transform transform);
        void CreateBlur();
        void CreateRemainingMovesPanel(Transform remainingMovesPanelParent);
        void CreateRotationPanel(Transform rotationPanelParent);
        GainStoreItemPanel CreateGainStoreItemPanel(GainStoreItemType gainType, Transform transform);
        void CreateAdditionBonusOfferItem(AdditionalBonusType additionalBonusType, Transform transform);
        void CreateLockIcon(Transform transform);
    }
}