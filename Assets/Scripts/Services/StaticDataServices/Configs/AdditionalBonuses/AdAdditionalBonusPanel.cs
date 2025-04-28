using Assets.Scripts.Data.Map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Zenject;

namespace Assets.Scripts.Services.StaticDataServices.Configs.AdditionalBonuses
{
    public class AdAdditionalBonusPanel : MonoBehaviour
    {
        private const int RewardID = 4;

        [SerializeField] private uint _buldozerItemsCount;
        [SerializeField] private uint _replaceItemsCount;
        [SerializeField] private TMP_Text _buldozerItemsCountValue;
        [SerializeField] private TMP_Text _replaceItemsCountValue;
        [SerializeField] private Button _button;

        private IMapData _worldData;

        [Inject]
        private void Construct(IMapData mapData)
        {
            _worldData = mapData;

            _buldozerItemsCountValue.text = "+" + _buldozerItemsCount.ToString();
            _replaceItemsCountValue.text = "+" + _replaceItemsCount.ToString();

            _button.onClick.AddListener(OnButtonClicked);
            YandexGame.RewardVideoEvent += OnRewarded;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
            YandexGame.RewardVideoEvent -= OnRewarded;
        }

        private void OnButtonClicked()
        {
            YandexGame.RewVideoShow(RewardID);
        }

        private void OnRewarded(int id)
        {
            if (id == RewardID)
            {
                _worldData.BulldozerItems.AddItems(_buldozerItemsCount);
                _worldData.ReplaceItems.AddItems(_replaceItemsCount);
                Destroy(gameObject);
            }
        }
    }
}