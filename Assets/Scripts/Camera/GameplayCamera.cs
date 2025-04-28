using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Camera
{
    public class GameplayCamera : MonoBehaviour
    {
        private const int OrthographicSize = 20;

        [SerializeField] private UnityEngine.Camera _mainCamera;

        private AnimationsConfig _animationsConfig;
        private IPersistantProgrss _persistentProgressService;

        private Tween _mover;

        public UnityEngine.Camera MainCamera => _mainCamera;

        [Inject]
        private void Construct(IStaticDataService staticDataService, IPersistantProgrss persistentProgressService)
        {
            _animationsConfig = staticDataService.AnimationsConfig;
            _persistentProgressService = persistentProgressService;

            _mainCamera.orthographicSize = OrthographicSize;

            ChangeOrthographic();

            _persistentProgressService.Progress.SettingsData.OrthographicChanged += ChangeOrthographic;
        }

        protected virtual void OnDestroy()
        {
            _persistentProgressService.Progress.SettingsData.OrthographicChanged -= ChangeOrthographic;
            _mover?.Kill();
        }

        public void MoveTo(Vector3 position, TweenCallback callback = null)
        {
            _mover = transform.DOMove(position, _animationsConfig.CameraMoveDuration);
            _mover.onComplete += callback;
        }

        private void ChangeOrthographic()
        {
            _mainCamera.orthographic = _persistentProgressService.Progress.SettingsData.IsOrthographicCamera;
        }

        public class Factory : PlaceholderFactory<GameplayCamera>
        {

        }
    }
}
