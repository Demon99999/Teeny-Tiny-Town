using Assets.Scripts.UI.Screens.Map.Panels.Store;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs.Store
{
    [CreateAssetMenu(fileName = "BildingStoreItemsConfig", menuName = "StaticData/WorldStore/Create new buildings store list config", order = 51)]
    public class BuildingStoreItemsCofnig : ScriptableObject
    {
        [SerializeField] private BuildingStoreItem _assetReference;

        public BuildingStoreItemConfig[] Configs;

        public BuildingStoreItem AssetReference => _assetReference;
    }
}
