using System;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Services.SaveLoadServices;

namespace Assets.Scripts.GameLogic.Map.Representation
{
    public class WorldRepresentationChanger
    {
        private readonly IWorldChanger _worldChanger;
        private readonly IMapFactory _mapFactory;
        private readonly NextBuildingForPlacingCreator _nextBuildingForPlacingCreator;
        private readonly ISaveLoadService _saveLoadService;

        public WorldRepresentationChanger(
            IWorldChanger worldChanger,
            IMapFactory mapFactory,
            NextBuildingForPlacingCreator nextBuildingForPlacingCreator,
            ISaveLoadService saveLoadService)
        {
            _worldChanger = worldChanger;

            _worldChanger.TilesChanged += OnTilesChanged;

            _mapFactory = mapFactory;
            _nextBuildingForPlacingCreator = nextBuildingForPlacingCreator;
            _saveLoadService = saveLoadService;
        }

        ~WorldRepresentationChanger()
        {
            _worldChanger.TilesChanged -= OnTilesChanged;
        }

        public event Action GameplayMoved;

        public TileRepresentation StartTile => WorldGenerator.GetTile(_nextBuildingForPlacingCreator.BuildingsForPlacingData.StartGridPosition);

        private WorldGenerator WorldGenerator => _mapFactory.WorldGenerator;

        private void OnTilesChanged()
        {
            GameplayMoved?.Invoke();
            _saveLoadService.SaveProgress();
        }
    }
}
