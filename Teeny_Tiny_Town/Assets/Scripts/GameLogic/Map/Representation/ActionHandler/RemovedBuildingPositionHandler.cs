using System;
using Assets.Scripts.GameLogic.Map.Representation.Markers;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using Assets.Scripts.GameLogic.Mover;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Map.Representation.ActionHandler
{
    public class RemovedBuildingPositionHandler : WorldActionHandlerState
    {
        private readonly MarkersVisibility _markersVisibility;

        private TileRepresentation _selectedTile;

        public RemovedBuildingPositionHandler(
            SelectFrame selectFrame,
            LayerMask layerMask,
            IGameplayMover gameplayMover,
            MarkersVisibility markersVisibility)
            : base(selectFrame, layerMask, gameplayMover)
        {
            _markersVisibility = markersVisibility;
        }

        public event Action Entered;
        public event Action Exited;

        public override void Enter()
        {
            Entered?.Invoke();
        }

        public override void Exit()
        {
            _selectedTile = null;
            Exited?.Invoke();
        }

        public override void OnHandleMoved(Vector2 handlePosition)
        {
            if (CheckTileIntersection(handlePosition, out TileRepresentation tile) && tile.IsEmpty == false)
            {
                if (_selectedTile != tile)
                {
                    SelectFrame.Select(tile);
                    _markersVisibility.SetSelectFrameShowed(true);
                    tile.ShakeBuilding();
                    ChangeSelectedTile(tile);
                }
            }
            else
            {
                _markersVisibility.SetSelectFrameShowed(false);
                ChangeSelectedTile(null);
            }
        }

        public override void OnPressed(Vector2 handlePosition)
        {
            if (CheckTileIntersection(handlePosition, out TileRepresentation tile) && tile.IsEmpty == false)
            {
                _markersVisibility.SetSelectFrameShowed(false);
                GameplayMover.RemoveBuilding(tile.GridPosition);
            }
        }

        private void ChangeSelectedTile(TileRepresentation tile)
        {
            _selectedTile?.StopBuildingShaking();
            _selectedTile = tile;
            _selectedTile?.ShakeBuilding();
        }
    }
}
