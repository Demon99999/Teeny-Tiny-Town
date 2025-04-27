using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs.Screens
{
    [CreateAssetMenu(fileName = "ScreensConfig", menuName = "StaticData/Create new screen config", order = 51)]
    public class ScreensConfig : ScriptableObject
    {
        [SerializeField] private ScreenConfig[] _configs;

        public ScreenConfig[] Configs => _configs;
    }
}