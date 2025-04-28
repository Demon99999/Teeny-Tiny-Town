using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.AssetPro;
using Assets.Scripts.Infrastructure.StateMachine;
using Assets.Scripts.Services.Input;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.SaveLoadSerices;
using Assets.Scripts.Services.StaticDataServices;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Assets.Scripts.GameLogic
{
    public class GlobalInstaller : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private AudioMixer _audioMixer;

        public override void InstallBindings()
        {
            Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();
            Container.BindInterfacesAndSelfTo<PersistantProgrss>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
            Container.BindInterfacesAndSelfTo<StaticDataServices>().AsSingle();
            Container.BindInterfacesAndSelfTo<SaveLoadServices>().AsSingle();
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
            Container.BindInstance(_audioMixer).AsSingle();
            GameStateMachineInstaller.Install(Container);
        }
    }
}