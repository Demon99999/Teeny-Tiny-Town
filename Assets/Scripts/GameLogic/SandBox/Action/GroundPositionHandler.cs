using Assets.Scripts.Data;
using Assets.Scripts.GameLogic.Map.Representation.ActionHandler;
using Assets.Scripts.GameLogic.Map.Representation.Markers;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using UnityEngine;

namespace Assets.Scripts.GameLogic.SandBox.Action
{
    public class GroundPositionHandler : ActionHandlerState
    {
        private readonly SandboxChanger _sandboxChanger;

        private bool _isPressed;
        private SandboxGroundType _groundType;
        public TileRepresentation _placedTile;

        public GroundPositionHandler(SelectFrame selectFrame, LayerMask layerMask, SandboxChanger sandboxChanger)
            : base(selectFrame, layerMask)
        {
            _sandboxChanger = sandboxChanger;
        }

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

        public void SetGround(SandboxGroundType type)
        {
            _groundType = type;
        }

        public override void OnHandleMoved(Vector2 handlePosition)
        {
            if (_isPressed)
            {
                CreateGround(handlePosition);
            }
        }

        public override void OnPressed(Vector2 handlePosition)
        {
            SelectFrame.Hide();
            _isPressed = false;
            _placedTile = null;
        }

        public override void OnHandlePressedMoveStarted(Vector2 handlePosition)
        {
            CreateGround(handlePosition);

            _isPressed = true;
        }

        private void CreateGround(Vector2 handlePosition)
        {
            if (CheckTileIntersection(handlePosition, out TileRepresentation tile) && tile != _placedTile)
            {
                SelectFrame.Select(tile);
                SelectFrame.Show();
                _placedTile = tile;
                _sandboxChanger.PutGround(tile.GridPosition, _groundType);
            }
        }
    }
}
