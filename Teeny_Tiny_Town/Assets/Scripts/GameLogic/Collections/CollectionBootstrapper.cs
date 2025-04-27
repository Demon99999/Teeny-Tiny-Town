using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Services.StaticDataServices.Configs.Screens;
using Assets.Scripts.UI;
using Zenject;

namespace Assets.Scripts.GameLogic.Collections
{
    public class CollectionBootstrapper : IInitializable
    {
        private readonly IUIFactory _uiFactory;
        private readonly IMapFactory _mapFactory;
        private readonly IGameFactory _gameplayFactory;

        public CollectionBootstrapper(IUIFactory uiFactory, IMapFactory mapFactory, IGameFactory gameplayFactory)
        {
            _uiFactory = uiFactory;
            _mapFactory = mapFactory;
            _gameplayFactory = gameplayFactory;
        }

        public void Initialize()
        {
            _gameplayFactory.CreatePlane();
            _gameplayFactory.CreateUiSoundPlayer();
            _mapFactory.CreateCollectionItemCreator();

            ScreenBase collectionWindow = _uiFactory.CreateScreen(ScreenType.Collection);

            collectionWindow.Open();
        }
    }
}
