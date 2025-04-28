using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices.Configs.Screens;
using Assets.Scripts.UI;

namespace Assets.Scripts.GameLogic.Map.Screens
{
    public class EducationWorldWindows : CurrencyWorldWindows
    {
        public EducationWorldWindows(IPersistantProgrss persistentProgressService, ScreensSwitcher windowsSwitcher,
            IUIFactory uiFactory)
            : base(persistentProgressService, windowsSwitcher, uiFactory)
        {

        }

        protected override ScreenType GameplayWindowType => ScreenType.Education;
    }
}
