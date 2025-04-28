using Assets.Scripts.GameLogic.Map;

namespace Assets.Scripts.GameLogic.SandBox
{
    public class SandboxRotation : IMapRotation
    {
        private SandboxWorld _sandboxWorld;

        public float RotationDegrees => _sandboxWorld.Rotation;

        public void Init(SandboxWorld sandboxWorld)
        {
            _sandboxWorld = sandboxWorld;
        }
    }
}
