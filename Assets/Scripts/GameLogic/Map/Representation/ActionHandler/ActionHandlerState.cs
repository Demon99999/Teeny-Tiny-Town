using Assets.Scripts.GameLogic.Map.Representation.Markers;
using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using Assets.Scripts.Infrastructure.StateMachine.State;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Map.Representation.ActionHandler
{
    public abstract class ActionHandlerState : IState
    {
        private const float RaycastDistance = 10000;

        protected readonly SelectFrame SelectFrame;

        private readonly LayerMask _layerMask;
        private readonly UnityEngine.Camera _camera;

        public ActionHandlerState(SelectFrame selectFrame, LayerMask layerMask)
        {
            SelectFrame = selectFrame;
            _layerMask = layerMask;

            _camera = UnityEngine.Camera.main;
        }

        public abstract void Enter();

        public abstract void Exit();

        public abstract void OnHandleMoved(Vector2 handlePosition);

        public abstract void OnPressed(Vector2 handlePosition);

        public virtual void OnHandlePressedMovePerformed(Vector2 handlePosition)
        {

        }

        public virtual void OnHandlePressedMoveStarted(Vector2 handlePosition)
        {

        }

        protected bool CheckTileIntersection(Vector2 handlePosition, out TileRepresentation tile)
        {
            tile = null;

            Ray ray = GetRay(handlePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, RaycastDistance, _layerMask, QueryTriggerInteraction.Ignore)
                && hitInfo.transform.TryGetComponent(out GroundCollider groundCollider))
            {
                tile = groundCollider.Tile;
                return true;
            }

            return false;
        }

        protected Ray GetRay(Vector2 handlePosition)
        {
            return _camera.ScreenPointToRay(new Vector3(handlePosition.x, handlePosition.y, 1));
        }
    }
}
