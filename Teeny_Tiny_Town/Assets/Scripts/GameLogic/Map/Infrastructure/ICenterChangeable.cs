using System;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Map.Infrastructure
{
    public interface ICenterChangeable
    {
        event Action<Vector2Int, bool> CenterChanged;
    }
}