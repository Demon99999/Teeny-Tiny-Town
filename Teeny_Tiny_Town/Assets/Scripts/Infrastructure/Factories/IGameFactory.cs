using Assets.Scripts.Camera;
using Assets.Scripts.GameLogic.Map;
using Assets.Scripts.GameLogic.SandBox;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Factories
{
    public interface IGameFactory
    {
        void CreatePlane();
        MapSwitcher CreateMapSwitcher();
        Map CreateEducationWorld(Vector3 position, Transform parent);
        Map CreateMap(string id, Vector3 position, Transform parent);
        GameplayCamera CreateCamera();
        void CreateUiSoundPlayer();
        void CreateWorldWalletSoundPlayer();
        //SandboxWorld CreateSandboxWorld();
    }
}