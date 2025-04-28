using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices.Configs.Screens;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Screens.Map;

namespace Assets.Scripts.GameLogic.Map.Screens
{
    public class WorldWindows : IWorldWindows
    {
        private readonly IPersistantProgrss _persistentProgressService;
        protected readonly ScreensSwitcher ScreensSwitcher;
        protected readonly IUIFactory UiFactory;

        public WorldWindows(IPersistantProgrss persistentProgressService, ScreensSwitcher screensSwitcher, IUIFactory uiFactory)
        {
            _persistentProgressService = persistentProgressService;
            ScreensSwitcher = screensSwitcher;
            UiFactory = uiFactory;

            IsRegistered = false;
        }

        public bool IsRegistered { get; private set; }
        protected virtual ScreenType GameplayWindowType => ScreenType.Gameplay;

        public virtual void Register()
        {
            ScreensSwitcher.RegisterScreen<AdditionalBonusOfferWindow>(ScreenType.AdditionalBonusOffer, UiFactory);
            ScreensSwitcher.RegisterScreen<GameplayWindow>(GameplayWindowType, UiFactory);
            ScreensSwitcher.RegisterScreen<RewardWindow>(ScreenType.Reward, UiFactory);
            ScreensSwitcher.RegisterScreen<ResultWindow>(ScreenType.Result, UiFactory);
            ScreensSwitcher.RegisterScreen<WorldQuestsWindow>(ScreenType.MapQuests, UiFactory);
            ScreensSwitcher.RegisterScreen<SaveGameplayWindow>(ScreenType.SaveGameplay, UiFactory);

            if (_persistentProgressService.Progress.StoreData.IsInfinityMovesUnlocked == false)
            {
                ScreensSwitcher.RegisterScreen<WaitingWindow>(ScreenType.Waiting, UiFactory);
            }

            IsRegistered = true;
        }

        public virtual void Remove()
        {
            ScreensSwitcher.Remove<AdditionalBonusOfferWindow>();
            ScreensSwitcher.Remove<GameplayWindow>();
            ScreensSwitcher.Remove<RewardWindow>();
            ScreensSwitcher.Remove<ResultWindow>();
            ScreensSwitcher.Remove<WorldQuestsWindow>();
            ScreensSwitcher.Remove<SaveGameplayWindow>();

            if (_persistentProgressService.Progress.StoreData.IsInfinityMovesUnlocked == false)
            {
                ScreensSwitcher.Remove<WaitingWindow>();
            }

            IsRegistered = false;
        }
    }
}
