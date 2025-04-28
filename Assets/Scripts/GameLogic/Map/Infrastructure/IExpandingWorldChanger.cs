using UnityEngine;

namespace Assets.Scripts.GameLogic.Map.Infrastructure
{
    public interface IExpandingWorldChanger : IWorldChanger
    {
        void Expand(Vector2Int newSize);
    }
}