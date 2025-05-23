﻿using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.GameLogic.Collections;
using Assets.Scripts.Infrastructure.State;
using Assets.Scripts.Infrastructure.StateMachine.State;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens
{
    public class CollectionWindow : ScreenBase
    {
        private const string LockedName = "?????";
        private const string LockedTitle = "???? ???? ????";

        [SerializeField] private Button _showNextItemButton;
        [SerializeField] private Button _showPreviousItemButton;
        [SerializeField] private TMP_Text _unlockedBuildingsQuentityValue;
        [SerializeField] private CanvasGroup _placedBuildingsQuantityPanel;
        [SerializeField] private TMP_Text _placedBuildingsQuantityValue;
        [SerializeField] private TMP_Text _buildingName;
        [SerializeField] private TMP_Text _buildingTitle;
        [SerializeField] private Button _hideButton;

        private CollectionItemCreator _collectionItemCreator;
        private IPersistantProgrss _persistentProgressService;
        private IStaticDataService _staticDataService;
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(
            CollectionItemCreator collectionItemCreator,
            IPersistantProgrss persistentProgressService,
            IStaticDataService staticDataService,
            GameStateMachine gameStateMachine)
        {
            _collectionItemCreator = collectionItemCreator;
            _persistentProgressService = persistentProgressService;
            _staticDataService = staticDataService;
            _gameStateMachine = gameStateMachine;

            int unlockedBuildingsCount = _persistentProgressService.Progress.BuildingDatas.Count(data => data.IsUnlocked);
            int buildingsCount = _persistentProgressService.Progress.BuildingDatas.Length;

            _unlockedBuildingsQuentityValue.text = $"{unlockedBuildingsCount}/{buildingsCount}";

            OnItemChanged(_persistentProgressService.Progress.BuildingDatas[_collectionItemCreator.CollectionItemIndex]);

            _showNextItemButton.onClick.AddListener(OnShowNextItemButtonClicked);
            _showPreviousItemButton.onClick.AddListener(OnShowPreviousItemButtonClicked);
            _collectionItemCreator.ItemChanged += OnItemChanged;
            _hideButton.onClick.AddListener(OnHideButtonClicked);
        }

        private void OnDestroy()
        {
            _showNextItemButton.onClick.RemoveListener(OnShowNextItemButtonClicked);
            _showPreviousItemButton.onClick.RemoveListener(OnShowPreviousItemButtonClicked);
            _collectionItemCreator.ItemChanged -= OnItemChanged;
            _hideButton.onClick.RemoveListener(OnHideButtonClicked);
        }

        private void OnShowPreviousItemButtonClicked()
        {
            _collectionItemCreator.ShowPreviousBuilding();
        }

        private void OnShowNextItemButtonClicked()
        {
            _collectionItemCreator.ShowNextBuilding();
        }

        private void OnItemChanged(BuildingData buildingData)
        {
            BuildingConfig buildingConfig = _staticDataService.GetBuilding<BuildingConfig>(buildingData.Type);

            _placedBuildingsQuantityValue.text = buildingData.Count.ToString();
            _placedBuildingsQuantityPanel.alpha = buildingData.IsUnlocked ? 1 : 0;

            _buildingName.text = buildingData.IsUnlocked ? buildingConfig.Name : LockedName;
            _buildingTitle.text = buildingData.IsUnlocked ? buildingConfig.Title : LockedTitle;
        }

        private void OnHideButtonClicked()
        {
            _gameStateMachine.Enter<GameLoopState>();
        }
    }
}