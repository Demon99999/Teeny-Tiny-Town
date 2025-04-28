using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Services.Input
{
    public class InputService : IInputService
    {
        private readonly InputActions _inputActions;

        private Vector2 _lastHandleMovePerformedPosition;
        private float _previousMagnitude;

        public InputService()
        {
            _inputActions = new InputActions();

            _inputActions.GamePlayInput.HandlePressedMove.started += OnHandlePressedMoveStarted;
            _inputActions.GamePlayInput.HandlePressedMove.performed += OnHandlePressedMovePerformed;
            _inputActions.GamePlayInput.HandlePressedMove.canceled += OnHandlePressedMoveCancled;
            _inputActions.GamePlayInput.HandleMove.performed += OnHandleMovePerformed;

            _inputActions.GameplayWindowsInput.UndoButtonPressed.performed += ctx => UndoButtonPressed?.Invoke();
            _inputActions.GameplayWindowsInput.RemoveBuildingButtonPressed.performed += ctx => RemoveBuildingButtonPressed?.Invoke();
            _inputActions.GameplayWindowsInput.ReplaceBuildingButtonPressed.performed += ctx => ReplaceBuildingButtonPressed?.Invoke();

            _inputActions.SandboxWindowsInput.ClearTilesButtonPressed.performed += ctx => ClearTilesButtonPressed?.Invoke();
            _inputActions.SandboxWindowsInput.BuildingsButtonPressed.performed += ctx => BuildingsButtonPressed?.Invoke();
            _inputActions.SandboxWindowsInput.GroundsButtonPressed.performed += ctx => GroundsButtonPressed?.Invoke();

            _inputActions.SandboxInput.MouseScroll.performed += OnMouseZoomed;
            TouchscreenZoom();

            _inputActions.SandboxInput.RotateWorld.performed += ctx => Rotated?.Invoke(ctx.ReadValue<Vector2>().x);
        }

        public event Action<Vector2> HandlePressedMoveStarted;
        public event Action<Vector2> HandlePressedMovePerformed;
        public event Action<Vector2> Pressed;
        public event Action<Vector2> HandleMoved;

        public event Action UndoButtonPressed;
        public event Action RemoveBuildingButtonPressed;
        public event Action ReplaceBuildingButtonPressed;

        public event Action ClearTilesButtonPressed;
        public event Action BuildingsButtonPressed;
        public event Action GroundsButtonPressed;

        public event Action<float> Zoomed;
        public event Action<float> Rotated;

        public void Dispose()
        {
            _inputActions.GamePlayInput.HandlePressedMove.started -= OnHandlePressedMoveStarted;
            _inputActions.GamePlayInput.HandlePressedMove.performed -= OnHandlePressedMovePerformed;
            _inputActions.GamePlayInput.HandlePressedMove.canceled -= OnHandlePressedMoveCancled;
            _inputActions.GamePlayInput.HandleMove.performed -= OnHandleMovePerformed;
            _inputActions.SandboxInput.MouseScroll.performed -= OnMouseZoomed;

            _inputActions.Disable();
        }

        public void SetEnabled(bool enabled)
        {
            if (enabled)
                _inputActions.Enable();
            else
                _inputActions.Disable();
        }

        private void OnHandleMovePerformed(InputAction.CallbackContext callbackContext)
        {
            _lastHandleMovePerformedPosition = callbackContext.ReadValue<Vector2>();
            HandleMoved?.Invoke(_lastHandleMovePerformedPosition);
        }

        private void OnHandlePressedMoveCancled(InputAction.CallbackContext obj)
        {
            Pressed?.Invoke(_lastHandleMovePerformedPosition);
        }

        private void OnHandlePressedMoveStarted(InputAction.CallbackContext obj)
        {
            HandlePressedMoveStarted?.Invoke(_lastHandleMovePerformedPosition);
        }

        private void OnHandlePressedMovePerformed(InputAction.CallbackContext obj)
        {
            HandlePressedMovePerformed?.Invoke(_lastHandleMovePerformedPosition);
        }

        private void OnMouseZoomed(InputAction.CallbackContext ctx)
        {
            float zoomValue = Mathf.Clamp(ctx.ReadValue<Vector2>().y, -1, 1);

            Zoomed?.Invoke(zoomValue);
        }

        private void TouchscreenZoom()
        {
            _inputActions.SandboxInput.SecondTouch.performed += _ =>
            {
                float magnitude = _inputActions.SandboxInput.FirstTouch.ReadValue<Vector2>().y - _inputActions.SandboxInput.SecondTouch.ReadValue<Vector2>().y;

                if (_previousMagnitude == 0)
                {
                    _previousMagnitude = magnitude;
                }

                float difference = magnitude - _previousMagnitude;

                _previousMagnitude = magnitude;

                float zoomValue = Mathf.Clamp(difference, -1, 1);

                Zoomed?.Invoke(zoomValue);
            };

            _inputActions.SandboxInput.SecondTouch.canceled += _ =>
            {
                _previousMagnitude = 0;
            };
        }
    }
}
