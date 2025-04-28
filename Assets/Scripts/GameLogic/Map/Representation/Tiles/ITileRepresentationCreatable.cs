using Assets.Scripts.Services.StaticDataServices.Configs.Maps;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Map.Representation.Tiles
{
    public interface ITileRepresentationCreatable
    {
        TileRepresentation Create(Vector2Int gridPosition, TileType tileType);
    }
}