using UnityEngine;

namespace Assets.Scripts.GameLogic.Mover
{
    public interface IExpandingGameplayMover : ICurrencyGameplayMover
    {
        void ExpandWorld(Vector2Int targetSize);
    }
}