using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Map.Infrastructure
{
    public interface ITileGetable
    {
        Tile GetTile(Vector2Int gridPosition);
        IEnumerable<int> GetLineNeighbors(int linePosition);
    }
}