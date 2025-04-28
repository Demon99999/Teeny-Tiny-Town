using System;
using System.Collections;
using System.Linq;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic.Map
{
    public class MapSwitcher : MonoBehaviour
    {
        [SerializeField] private float _changingSpeed;

        private Vector3 _currentMapPosition;
        private Vector3 _previousMapPosition;
        private Vector3 _nextMapdPosition;
        private IGameFactory _gameFactory;
        private IPersistantProgrss _persistentProgressService;
        private IStaticDataService _staticDataService;
        private Map _currentMap;
        private bool _isWorldChanged;

        public event Action<string> CurrentWorldChanged;

        public string CurrentWorldDataId { get; private set; }

        [Inject]
        private void Construct(IStaticDataService staticDataService, IGameFactory gameplayFactory, IPersistantProgrss persistentProgressService)
        {
            _staticDataService = staticDataService;
            _currentMapPosition = _staticDataService.MapsConfig.CurrentWorldPosition;

            _gameFactory = gameplayFactory;
            _persistentProgressService = persistentProgressService;

            float distanceBetweenWorlds = _staticDataService.MapsConfig.DistanceBetweenWorlds;
            _previousMapPosition = new Vector3(_currentMapPosition.x - distanceBetweenWorlds, _currentMapPosition.y, _currentMapPosition.z);
            _nextMapdPosition = new Vector3(_currentMapPosition.x + distanceBetweenWorlds, _currentMapPosition.y, _currentMapPosition.z);

            _isWorldChanged = false;
        }

        public void CleanCurrentWorld()
        {
            _currentMap.Clean();
        }

        public void Initialize()
        {
            if (_persistentProgressService.Progress.IsEducationCompleted)
            {
                _currentMap = _gameFactory.CreateMap(_persistentProgressService.Progress.LastPlayedWorldDataId, _currentMapPosition, transform);
            }
            else
            {
                _currentMap = _gameFactory.CreateEducationWorld(_currentMapPosition, transform);
            }

            CurrentWorldDataId = _persistentProgressService.Progress.LastPlayedWorldDataId;
        }

        public void ShowNextWorld()
        {
            MapConfig config = _staticDataService.GetMap<MapConfig>(CurrentWorldDataId);
            ChangeWorld(config.NextWorldId, _nextMapdPosition, _previousMapPosition);
        }

        public void ShowPreviousWorld()
        {
            MapConfig config = _staticDataService.GetMap<MapConfig>(CurrentWorldDataId);
            ChangeWorld(config.PreviousWorldId, _previousMapPosition, _nextMapdPosition);
        }

        public void StartLastPlayedWorld()
        {
            StartCoroutine(StartLastPlayedWorldRoutine());
        }

        public void StartLastWorld()
        {
            MapConfig config = _staticDataService.GetMap<MapConfig>(CurrentWorldDataId);

            if (config is ExpandingMapConfig expandingConfig)
            {
                Destroy(_currentMap.gameObject);
                _persistentProgressService.Progress.CurrencyMapDatas[int.Parse(CurrentWorldDataId)].Size = expandingConfig.StartSize;
                _currentMap = _gameFactory.CreateMap(config.Id, _currentMapPosition, transform);
                _currentMap.Enter();
            }
            else
            {
                StartLastPlayedWorld();
            }
        }

        public void StartCurrentWorld()
        {
            StartCoroutine(StartCurrentWorldRoutine());
        }

        public void ChangeToEducationWorld(Action callback)
        {
            _isWorldChanged = true;

            MapConfig worldConfig = _staticDataService.GetMap<MapConfig>(_staticDataService.MapsConfig.EducationMapdId);
            CurrentWorldDataId = _staticDataService.MapsConfig.EducationMapdId;
            _persistentProgressService.Progress.LastPlayedWorldDataId = CurrentWorldDataId;

            _persistentProgressService.Progress.GetMapData(CurrentWorldDataId).Update(worldConfig.TilesDatas, worldConfig.NextBuildingTypeForCreation, worldConfig.StartingAvailableBuildingTypes.ToList());

            Map map = _gameFactory.CreateEducationWorld(_nextMapdPosition, transform);

            ReplaceWorlds(map, _previousMapPosition, _currentMapPosition, () =>
            {
                _currentMap = map;
                _isWorldChanged = false;
                CurrentWorldChanged?.Invoke(CurrentWorldDataId);
                callback?.Invoke();
            });
        }

        private void ChangeWorld(string targetWorldDataId, Vector3 newWorldStartPosition, Vector3 currentMapTargetPosition)
        {
            if (_isWorldChanged)
                return;

            _isWorldChanged = true;

            CurrentWorldDataId = targetWorldDataId;
            CurrentWorldChanged?.Invoke(CurrentWorldDataId);

            Map map = _gameFactory.CreateMap(targetWorldDataId, newWorldStartPosition, transform);

            ReplaceWorlds(map, currentMapTargetPosition, _currentMapPosition, () =>
            {
                _currentMap = map;
                _isWorldChanged = false;
            });
        }

        private void ReplaceWorlds(Map changedMap, Vector3 currentMapTargetPosition, Vector3 changedMapTargetPosition, TweenCallback callback)
        {
            Map currentMap = _currentMap;

            currentMap.MoveTo(currentMapTargetPosition, callback: () => Destroy(currentMap.gameObject));
            changedMap.MoveTo(changedMapTargetPosition, callback);
        }

        private IEnumerator StartLastPlayedWorldRoutine()
        {
            if (_isWorldChanged)
            {
                yield return new WaitWhile(() => _isWorldChanged);
            }

            if (CurrentWorldDataId != _persistentProgressService.Progress.LastPlayedWorldDataId)
            {
                ChangeWorld(
                    _persistentProgressService.Progress.LastPlayedWorldDataId,
                    _nextMapdPosition,
                    _previousMapPosition);

                yield return new WaitWhile(() => _isWorldChanged);
            }

            _currentMap.Enter();
        }

        private IEnumerator StartCurrentWorldRoutine()
        {
            if (_isWorldChanged)
            {
                yield return new WaitWhile(() => _isWorldChanged);
            }

            _currentMap.Enter();
        }

        public class Factory : PlaceholderFactory<MapSwitcher>
        {

        }
    }
}
