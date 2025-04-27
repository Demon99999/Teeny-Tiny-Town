using System;
using Assets.Scripts.Data.Map;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Reward;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map.Reward
{
    public class RewardPanel : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _countValue;

        private IStaticDataService _staticDataService;
        private IMapData _mapData;

        public RewardType Type { get; private set; }
        public uint RewardCount { get; private set; }

        public event Action<RewardPanel> Clicked;

        [Inject]
        private void Construct(IStaticDataService staticDataService, IMapData worldData)
        {
            _staticDataService = staticDataService;
            _mapData = worldData;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        public void Init(Sprite icon, RewardType rewardType)
        {
            _icon.sprite = icon;
            Type = rewardType;

            RewardCount = _staticDataService.GetReward(Type).GetRewardCount(_mapData);

            _countValue.text = RewardCount.ToString();
        }

        private void OnButtonClicked()
        {
            Clicked?.Invoke(this);
        }

        public class Factory : PlaceholderFactory<RewardPanel> { }
    }
}
