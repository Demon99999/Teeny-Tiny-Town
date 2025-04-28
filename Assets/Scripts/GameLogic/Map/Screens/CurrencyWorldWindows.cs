using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices.Configs.Screens;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Screens.Map;

namespace Assets.Scripts.GameLogic.Map.Screens
{
    public class CurrencyWorldWindows : WorldWindows, IWorldWindows
    {
        public CurrencyWorldWindows(
            IPersistantProgrss persistentProgressService,
            ScreensSwitcher windowsSwitcher,
            IUIFactory uiFactory)
            : base(persistentProgressService, windowsSwitcher, uiFactory)
        {
        }

        protected override ScreenType GameplayWindowType => ScreenType.CurrencyGameplay;

        public override void Register()
        {
            base.Register();

            ScreensSwitcher.RegisterScreen<StoreWindow>(ScreenType.MapStore, UiFactory);
            ScreensSwitcher.RegisterScreen<GainBuyingWindow>(ScreenType.GainBuying, UiFactory);
        }

        public override void Remove()
        {
            base.Remove();

            ScreensSwitcher.Remove<StoreWindow>();
            ScreensSwitcher.Remove<GainBuyingWindow>();
        }
    }
}
