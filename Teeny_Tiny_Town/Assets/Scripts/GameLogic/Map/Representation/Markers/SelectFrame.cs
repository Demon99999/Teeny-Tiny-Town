using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic.Map.Representation.Markers
{
    public class SelectFrame : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Vector3 _offset;

        public void Select(TileRepresentation tile)
        {
            if (tile == null)
            {
                Hide();

                return;
            }

            transform.position = tile.BuildingPoint.position + _offset;
        }

        public void Show()
        {
            _canvas.enabled = true;
        }

        public void Hide()
        {
            _canvas.enabled = false;
        }

        public class Factory : PlaceholderFactory<SelectFrame> { }
    }
}
