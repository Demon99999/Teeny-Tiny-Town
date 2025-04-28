using Assets.Scripts.Infrastructure.State;
using Assets.Scripts.Infrastructure.StateMachine;
using Assets.Scripts.Infrastructure.StateMachine.State;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure
{
    public class Bootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private GameStateMachine _gameStateMachine;
        private StatesFactory _statesFactory;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine, StatesFactory statesFactory)
        {
            _gameStateMachine = gameStateMachine;
            _statesFactory = statesFactory;
        }

        private void Awake()
        {
            _gameStateMachine.RegisterState(_statesFactory.Create<BootstrapState>());
            _gameStateMachine.RegisterState(_statesFactory.Create<GameLoopState>());
            _gameStateMachine.RegisterState(_statesFactory.Create<LoadProgressState>());
            _gameStateMachine.RegisterState(_statesFactory.Create<SandboxState>());
            _gameStateMachine.RegisterState(_statesFactory.Create<CollectionState>());
        }

        private void Start()
        {
            _gameStateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}