using UnityEngine;

namespace Assets.Scripts.GameLogic.Map.Representation.Tiles
{
    public class Ground : MonoBehaviour
    {
        [SerializeField] private Transform _buildingPoint;

        public Transform BuildingPoint => _buildingPoint;
    }
}