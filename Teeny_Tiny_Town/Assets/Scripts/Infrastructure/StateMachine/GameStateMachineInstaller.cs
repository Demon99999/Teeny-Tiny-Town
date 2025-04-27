using Assets.Scripts.Infrastructure.State;
using Zenject;

namespace Assets.Scripts.Infrastructure.StateMachine
{
    public class GameStateMachineInstaller : Installer<GameStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<StatesFactory>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle();
        }
    }
}