using System;
using UnityEngine;

namespace Assets.Scripts.Services.Input
{
    public interface IInputService
    {
        event Action<Vector2> HandleMoved;
        event Action<Vector2> HandlePressedMovePerformed;
        event Action<Vector2> HandlePressedMoveStarted;
        event Action<Vector2> Pressed;
        event Action UndoButtonPressed;
        event Action RemoveBuildingButtonPressed;
        event Action ReplaceBuildingButtonPressed;
        event Action ClearTilesButtonPressed;
        event Action BuildingsButtonPressed;
        event Action GroundsButtonPressed;
        event Action<float> Zoomed;
        event Action<float> Rotated;

        void SetEnabled(bool enabled);
    }
}