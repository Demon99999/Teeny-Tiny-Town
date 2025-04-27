using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.GameLogic.Map.Representation.ActionHandler;
using Assets.Scripts.GameLogic.Map.Screens;
using Assets.Scripts.GameLogic.Mover;
using Assets.Scripts.UI.Screens.Map.Panels.Store;

namespace Assets.Scripts.GameLogic.Map.Installer
{
    public class CurrencyWorldInstaller : WorldInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();

            BindGainBuyer();
            BindUnlimitedQuantityGainBuyer();
        }

        private void BindUnlimitedQuantityGainBuyer()
        {
            Container.Bind<UnlimitedQuantityGainBuyer>().AsSingle();
        }

        private void BindGainBuyer()
        {
            Container.Bind<GainBuyer>().AsSingle();
        }

        protected override void BindAcitonHandlerSwitcher()
        {
            Container.BindInterfacesTo<CurrencyWorldActionHandlerSwitcher>().AsSingle();
        }

        protected override void BindWorldWindows()
        {
            Container.BindInterfacesTo<CurrencyWorldWindows>().AsSingle();
        }

        protected override void BindWorldBootstrapper()
        {
            Container.BindInterfacesAndSelfTo<CurrencyWorldBootstrapper>().AsSingle().NonLazy();
        }

        protected override void BindWorldData()
        {
            MapData mapData = PersistentProgressService.Progress.GetMapData(MapDataId);
            var currencyMapData = mapData as CurrencyMapData;

            Container.BindInterfacesTo<CurrencyMapData>().FromInstance(currencyMapData).AsSingle();
        }

        protected override void BindGameplayMover()
        {
            Container.BindInterfacesTo<CurrencyGameplayMover>().AsSingle();
        }

        protected override void BindWorldChanger()
        {
            Container.BindInterfacesTo<CurrencyWorldChanger>().AsSingle();
        }
    }
}
