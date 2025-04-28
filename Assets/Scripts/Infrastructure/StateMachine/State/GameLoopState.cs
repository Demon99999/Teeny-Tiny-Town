using System;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices;

namespace Assets.Scripts.Infrastructure.StateMachine.State
{
    public class GameLoopState : IState
    {
        private const string GameScene = "Game";

        private readonly ISceneLoader _sceneLoader;
        private readonly IPersistantProgrss _persistentProgress;
        private readonly IStaticDataService _staticDataService;

        public GameLoopState(ISceneLoader sceneLoader, IPersistantProgrss persistantProgrss, IStaticDataService staticDataService)
        {
            _sceneLoader = sceneLoader;
            _persistentProgress = persistantProgrss;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            uint availableMovesCount = _staticDataService.MapsConfig.AvailableMovesCount;

            if (_persistentProgress.Progress.GameplayMovesCounter.RemainingMovesCount > availableMovesCount)
            {
                _persistentProgress.Progress.GameplayMovesCounter.SetCount(availableMovesCount);
            }

            _sceneLoader.Load(GameScene);
        }

        public void Exit()
        {

        }
    }
}