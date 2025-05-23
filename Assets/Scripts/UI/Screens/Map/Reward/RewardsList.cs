﻿using System;
using System.Collections.Generic;
using Assets.Scripts.GameLogic.Points;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices.Configs.Reward;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map.Reward
{
    public class RewardsList : MonoBehaviour
    {
        private const uint StartGameplayWalletValueReward = 8;
        private const uint StartGameplayWalletValueRewardIncrease = 2;

        [SerializeField] private Button _updateRewardsButton;
        [SerializeField] private TMP_Text _gameplayWalletValueRewardValue;

        private RewardsCreator _rewardsCreator;
        private IUIFactory _uiFactory;
        private Rewarder _rewarder;
        private IPersistantProgrss _persistentProgressService;

        private List<RewardPanel> _rewardPanels;
        private uint _gameplayWalletValueReward;

        public event Action RewardChoosed;

        [Inject]
        private void Construct(RewardsCreator rewardsCreator, IUIFactory uiFactory, Rewarder rewarder, IPersistantProgrss persistentProgressService)
        {
            _rewardsCreator = rewardsCreator;
            _uiFactory = uiFactory;
            _rewarder = rewarder;
            _persistentProgressService = persistentProgressService;

            _rewardPanels = new List<RewardPanel>();
            _gameplayWalletValueReward = StartGameplayWalletValueReward;

            _rewardsCreator.RewardsCreated += OnRewardsCreated;
            _updateRewardsButton.onClick.AddListener(OnUpdateRewardsButtonClicked);
        }

        private void OnDestroy()
        {
            _updateRewardsButton.onClick.RemoveListener(OnUpdateRewardsButtonClicked);
            _rewardsCreator.RewardsCreated -= OnRewardsCreated;

            foreach (RewardPanel rewardPanel in _rewardPanels)
            {
                rewardPanel.Clicked -= OnRewardPanelClicked;
            }
        }

        private void OnRewardsCreated(IReadOnlyList<RewardType> rewardTypes)
        {
            _gameplayWalletValueRewardValue.text = _gameplayWalletValueReward.ToString();

            foreach (RewardPanel rewardPanel in _rewardPanels)
            {
                rewardPanel.Clicked -= OnRewardPanelClicked;
                Destroy(rewardPanel.gameObject);
            }

            _rewardPanels.Clear();

            foreach (RewardType rewardType in rewardTypes)
            {
                RewardPanel rewardPanel = _uiFactory.CreateRewardPanel(rewardType, transform);
                _rewardPanels.Add(rewardPanel);
                rewardPanel.Clicked += OnRewardPanelClicked;
            }
        }

        private void OnRewardPanelClicked(RewardPanel rewardPanel)
        {
            _rewarder.Reward(rewardPanel.Type, rewardPanel.RewardCount);
            _persistentProgressService.Progress.Wallet.Give(_gameplayWalletValueReward);
            _gameplayWalletValueReward += StartGameplayWalletValueRewardIncrease;
            RewardChoosed?.Invoke();
        }

        private void OnUpdateRewardsButtonClicked()
        {
            _rewardsCreator.CreateRewards();
        }
    }
}