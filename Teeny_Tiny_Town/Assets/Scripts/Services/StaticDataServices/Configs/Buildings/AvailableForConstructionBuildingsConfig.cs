using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs.Buildings
{
    [CreateAssetMenu(fileName = "AvailableForConstructionBuildingsConfig", menuName = "StaticData/Create new available for construction buildings config", order = 51)]
    public class AvailableForConstructionBuildingsConfig : ScriptableObject
    {
        public uint RequiredCreatedBuildingsToAddNext;
        public uint AvailableUpgradeBrach;

        public BuildingUpgradeBranch[] Branches;

        private void OnValidate()
        {
            if (AvailableUpgradeBrach >= Branches.Length)
            {
                AvailableUpgradeBrach = 0;
            }
        }

        public bool TryFindeNextBuilding(BuildingType buildingType, out BuildingType nextBuildingType)
        {
            nextBuildingType = BuildingType.Undefined;

            foreach (BuildingUpgradeBranch branch in Branches)
            {
                foreach (BuildingUpgradeBranchElement branchElement in branch.Elements)
                {
                    if (branchElement.BuildingType == buildingType)
                    {
                        nextBuildingType = branchElement.NextBuildingType;
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
