using Assets.Scripts.GameLogic.SandBox;
using Assets.Scripts.UI.Screens.SandBox;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs.Maps.SandBox
{
    [CreateAssetMenu(fileName = "SandboxConfig", menuName = "StaticData/WorldConfig/Create new sandbox config", order = 51)]
    public class SandboxConfig : ScriptableObject
    {
        [SerializeField] private Vector2Int _size;
        [SerializeField] private SandboxGroundConfig[] _grounds;
        [SerializeField] private SandboxWorld _sandboxWorld;
        [SerializeField] private SandboxPanelElement _sandboxPanelElement;
        [SerializeField] private GameObject _lockSprite;

        public Vector2Int Size => _size;

        public SandboxGroundConfig[] Grounds => _grounds;

        public SandboxWorld SandboxWorld => _sandboxWorld;

        public SandboxPanelElement SandboxPanelElement => _sandboxPanelElement;

        public GameObject LockSprite => _lockSprite;
    }
}
