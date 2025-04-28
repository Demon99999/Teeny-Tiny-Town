using UnityEngine;

namespace Assets.Scripts.GameLogic.Map.Representation.Tiles
{
    public class GroundCollider : MonoBehaviour
    {
        public TileRepresentation Tile { get; private set; }

        private void Start()
        {
            Tile = GetComponentInParent<TileRepresentation>();
        }
    }
}