using Assets.Scripts.GameLogic.Map.Representation.ActionHandler;
using Assets.Scripts.GameLogic.Map.Representation.Markers;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using UnityEngine;

namespace Assets.Scripts.GameLogic.SandBox.Action
{
    public class BuildingPositionHandler : ActionHandlerState
    {
        private readonly SandboxChanger _sandboxChanger;
        private bool _isPressed;
        private BuildingType _buildingType;
        private TileRepresentation _placedTile;
        private bool _isBuildingSetted;

        public BuildingPositionHandler(SelectFrame selectFrame, LayerMask layerMask, SandboxChanger sandboxChanger)
            : base(selectFrame, layerMask)
        {
            _sandboxChanger = sandboxChanger;

            _isBuildingSetted = false;
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

        public void SetBuilding(BuildingType type)
        {
            _buildingType = type;
            _isBuildingSetted = true;
        }

        public override void OnHandleMoved(Vector2 handlePosition)
        {
            if (_isPressed)
            {
                TryCreateBuilding(handlePosition);
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
            TryCreateBuilding(handlePosition);

            _isPressed = true;
        }

        private void TryCreateBuilding(Vector2 handlePosition)
        {
            if (_isBuildingSetted == false)
            {
                return;
            }

            if (CheckTileIntersection(handlePosition, out TileRepresentation tile) && tile != _placedTile && tile.IsEmpty)
            {
                SelectFrame.Select(tile);
                SelectFrame.Show();
                _placedTile = tile;
                _sandboxChanger.PutBuilding(tile.GridPosition, _buildingType);
            }
        }
    }
}
