namespace Assets.Scripts.Infrastructure.StateMachine.State
{
    public class CollectionState : IState
    {
        private const string CollectionScene = "CollectionScene";

        private readonly ISceneLoader _sceneLoader;

        public CollectionState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.Load(CollectionScene);
        }

        public void Exit()
        {

        }
    }
}
