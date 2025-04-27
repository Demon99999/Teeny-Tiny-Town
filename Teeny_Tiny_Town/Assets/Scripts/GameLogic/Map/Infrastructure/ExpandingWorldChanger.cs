using System.Collections.Generic;
using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Map.Infrastructure
{
    public class ExpandingWorldChanger : CurrencyWorldChanger, IExpandingWorldChanger
    {
        private bool _isExpanded;
        private ITileRepresentationCreatable _tileRepresentationCreatable;
        
        public ExpandingWorldChanger(
            IStaticDataService staticDataService,
            ICurrencyMapData mapData,
            NextBuildingForPlacingCreator nextBuildingForPlacingCreator,
            IPersistantProgrss persistentProgressService)
            : base(staticDataService, mapData, nextBuildingForPlacingCreator, persistentProgressService)
        {
            _isExpanded = false;
        }

        public override void Generate(ITileRepresentationCreatable tileRepresentationCreatable)
        {
            _tileRepresentationCreatable = tileRepresentationCreatable;
            base.Generate(tileRepresentationCreatable);
        }

        public void Expand(Vector2Int newSize)
        {
            _isExpanded = true;

            Dispose();
            Clean();
            Fill(_tileRepresentationCreatable);
            OnCenterChanged(true);

            _isExpanded = false;
        }

        protected override List<Tile> CreateTiles(List<RoadTile> roadTiles, List<TallTile> tallTiles)
        {
            if (_isExpanded == false)
            {
                return base.CreateTiles(roadTiles, tallTiles);
            }

            List<Tile> tiles = new List<Tile>();

            foreach (TileData tileData in WorldData.Tiles)
            {
                if (IsTileFitsIntoGrid(tileData.GridPosition) == false)
                    continue;

                Tile tile = GetGround(roadTiles, tallTiles, tileData);

                tiles.Add(tile);
            }

            return tiles;
        }
    }
}