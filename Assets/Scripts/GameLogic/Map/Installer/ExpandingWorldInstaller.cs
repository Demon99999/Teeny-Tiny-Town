using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.GameLogic.Mover;

namespace Assets.Scripts.GameLogic.Map.Installer
{
    public class ExpandingWorldInstaller : CurrencyWorldInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();

            BindWorldExpander();
        }

        protected override void BindGameplayMover()
        {
            Container.BindInterfacesTo<ExpandingGameplayMover>().AsSingle();
        }

        protected override void BindWorldChanger()
        {
            Container.BindInterfacesTo<ExpandingWorldChanger>().AsSingle();
        }

        private void BindWorldExpander()
        {
            Container.BindInterfacesAndSelfTo<WorldExpander>().AsSingle().NonLazy();
        }
    }
}
