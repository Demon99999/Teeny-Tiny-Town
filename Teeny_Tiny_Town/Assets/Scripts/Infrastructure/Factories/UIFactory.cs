using System;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.AdditionalBonuses;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using Assets.Scripts.Services.StaticDataServices.Configs.Reward;
using Assets.Scripts.Services.StaticDataServices.Configs.Screens;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Screens.Map.Panels.Store;
using Assets.Scripts.UI.Screens.Map.Quest;
using Assets.Scripts.UI.Screens.Map.Reward;
using Assets.Scripts.Services.StaticDataServices.Configs.Store;
using Assets.Scripts.UI.Screens.Map.Panels;
using Assets.Scripts.UI.Screens.Map.Panels.AdditionalBonusOffer;
using Assets.Scripts.UI.Screens.Map.Panels.Store.GainPanels;
using Assets.Scripts.UI.Screens.SandBox;
using Assets.Scripts.UI.Screens.SelectionMap;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly DiContainer _container;
        private readonly Blur.BlurFactory _blurFactory;
        private readonly RemainingMovesPanel.Factory _remainingMovesPanelFactory;

        public UIFactory(
            IStaticDataService staticDataService,
            DiContainer diContainer,
            Blur.BlurFactory blurFactory,
            RemainingMovesPanel.Factory remainingMovesPanel)
        {
            _container = diContainer;
            _blurFactory = blurFactory;
            _staticDataService = staticDataService;
            _remainingMovesPanelFactory = remainingMovesPanel;
        }

        public void CreateBlur()
        {
            CreateAndBind(_blurFactory.Create, blur => blur.HideImmediately());
        }

        public ScreenBase CreateScreen(ScreenType screenType)
        {
            var screenPrefab = _staticDataService.GetScreen(screenType).Template;
            return CreateUIElement<ScreenBase>(screenPrefab, null, screen => screen.HideImmediately());
        }

        public QuestPanel CreateQuestPanel(string id, Transform parent)
        {
            var questPrefab = _staticDataService.QuestsConfig.QuestPanel;
            return CreateUIElement<QuestPanel>(questPrefab, parent, panel => panel.Init(id));
        }

        public void CreateRemainingMovesPanel(Transform parent)
        {
            CreateUIElement(_remainingMovesPanelFactory.Create, parent);
        }

        public void CreateRotationPanel(Transform parent)
        {
            var rotationPrefab = _staticDataService.MapsConfig.RotationPanel;
            CreateUIElement<RotationPanel>(rotationPrefab, parent);
        }

        public RewardPanel CreateRewardPanel(RewardType rewardType, Transform parent)
        {
            var config = _staticDataService.GetReward(rewardType);
            return CreateUIElement<RewardPanel>(config.PrefabPanel, parent,
                panel => panel.Init(config.IconAssetReference, rewardType));
        }

        public void CreateAdditionBonusOfferItem(AdditionalBonusType bonusType, Transform parent)
        {
            var config = _staticDataService.GetAdditionalBonus(bonusType);
            CreateUIElement<AdditionalBonusOfferItem>(config.PanelAssetReference, parent,
                item => item.Init(bonusType, config.IconAssetReference));
        }

        public GainStoreItemPanel CreateGainStoreItemPanel(GainStoreItemType gainType, Transform parent)
        {
            var config = _staticDataService.GetGainStoreItem(gainType);
            return CreateUIElement<GainStoreItemPanel>(config.PanelAssetReference, parent,
                panel => panel.Init(gainType, config.SpriteconAssetReference));
        }

        public BuildingStoreItem CreateStoreItem(BuildingType buildingType, Transform parent)
        {
            var config = _staticDataService.GetBuildingStoreItem(buildingType);
            return CreateUIElement<BuildingStoreItem>(_staticDataService.StoreItemsConfig.AssetReference, parent,
                item => item.Init(buildingType, config.IconAsset));
        }

        public SandboxPanelElement CreateSandboxPanelElement(Transform parent, Sprite icon)
        {
            return CreateUIElement<SandboxPanelElement>(_staticDataService.SandboxConfig.SandboxPanelElement, parent,
                element => element.Init(icon));
        }

        public void CreateLockIcon(Transform parent)
        {
            _container.InstantiatePrefab(_staticDataService.SandboxConfig.LockSprite, parent);
        }

        public PeculiarityIconPanel CreatePeculiarityIconPanel(Sprite icon, Transform parent)
        {
            return CreateUIElement<PeculiarityIconPanel>(_staticDataService.MapsConfig.PeculiarityIconPanel, parent,
                panel => panel.Init(icon));
        }

        private T CreateUIElement<T>(T prefab, Transform parent, Action<T> initialize = null) where T : Component
        {
            var instance = _container.InstantiatePrefab(prefab, parent);
            instance.transform.SetParent(parent, false);

            var component = instance.GetComponent<T>();
            
            initialize?.Invoke(component);
            return component;
        }

        private T CreateUIElement<T>(Func<T> factory, Transform parent, Action<T> initialize = null) where T : MonoBehaviour
        {
            var instance = factory();
            instance.transform.SetParent(parent, false);

            initialize?.Invoke(instance);
            return instance;
        }

        private T CreateAndBind<T>(Func<T> factory, Action<T> initialize = null) where T : MonoBehaviour
        {
            var instance = factory();
            _container.BindInstance(instance).AsSingle();

            initialize?.Invoke(instance);
            return instance;
        }
    }
}