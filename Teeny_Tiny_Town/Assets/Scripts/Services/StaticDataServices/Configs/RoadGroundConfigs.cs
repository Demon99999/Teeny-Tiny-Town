using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs
{
    [CreateAssetMenu(fileName = "RoadGroundConfigs", menuName = "StaticData/Create new road ground config", order = 51)]
    public class RoadGroundConfigs : ScriptableObject
    {
        public RoadGroundConfig[] Configs;
    }
}