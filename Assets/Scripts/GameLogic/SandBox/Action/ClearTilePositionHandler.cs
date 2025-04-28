using Assets.Scripts.GameLogic.Map.Representation.ActionHandler;
using Assets.Scripts.GameLogic.Map.Representation.Markers;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using UnityEngine;

namespace Assets.Scripts.GameLogic.SandBox.Action
{
    public class ClearTilePositionHandler : ActionHandlerState
    {
        private readonly SandboxChanger _sandboxChanger;

        private bool _isPressed;
        private TileRepresentation _clearedTile;

        public ClearTilePositionHandler(SelectFrame selectFrame, LayerMask layerMask, SandboxChanger sandboxChanger)
            : base(selectFrame, layerMask) =>
            _sandboxChanger = sandboxChanger;

        public event System.Action Entered;
        public event System.Action Exited;

        public override void Enter()
        {
            _isPressed = false;
            Entered?.Invoke();
        }

        public override void Exit()
        {
            Exited?.Invoke();
        }

        public override void OnHandleMoved(Vector2 handlePosition)
        {
            if (_isPressed)
            {
                ClearTile(handlePosition);
            }
        }

        public override void OnPressed(Vector2 handlePosition)
        {
            SelectFrame.Hide();
            _isPressed = false;
            _clearedTile = null;
        }

        public override void OnHandlePressedMoveStarted(Vector2 handlePosition)
        {
            ClearTile(handlePosition);

            _isPressed = true;
        }

        private void ClearTile(Vector2 handlePosition)
        {
            if (CheckTileIntersection(handlePosition, out TileRepresentation tile) && tile != _clearedTile)
            {
                SelectFrame.Select(tile);
                SelectFrame.Show();
                _clearedTile = tile;
                _sandboxChanger.ClearTile(tile.GridPosition);
            }
        }
    }
}