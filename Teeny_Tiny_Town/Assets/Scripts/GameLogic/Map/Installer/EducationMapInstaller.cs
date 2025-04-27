using Assets.Scripts.GameLogic.Map.Screens;

namespace Assets.Scripts.GameLogic.Map.Installer
{
    public class EducationMapInstaller : CurrencyWorldInstaller
    {
        protected override void BindWorldBootstrapper()
        {
            Container.BindInterfacesAndSelfTo<EducationWorldBootstrapper>().AsSingle().NonLazy();
        }

        protected override void BindWorldWindows()
        {
            Container.BindInterfacesTo<EducationWorldWindows>().AsSingle();
        }
    }
}
