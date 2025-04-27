using Assets.Scripts.Data.Map;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.AdditionalBonuses;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map.Panels.AdditionalBonusOffer
{
    public class AdditionalBonusOfferItem : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _countValue;
        [SerializeField] private TMP_Text _costValue;

        private IStaticDataService _staticDataService;
        private IPersistantProgrss _persistentProgressService;
        private IMapData _worldData;

        private AdditionalBonusType _type;
        private uint _cost;

        [Inject]
        private void Construct(IPersistantProgrss persistentPRogressService, IStaticDataService staticDataService, IMapData worldData)
        {
            _staticDataService = staticDataService;
            _persistentProgressService = persistentPRogressService;
            _worldData = worldData;

            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDestroy() =>
            _button.onClick.RemoveListener(OnButtonClicked);

        private void OnButtonClicked()
        {
            if (_persistentProgressService.Progress.Wallet.TryGet(_cost))
            {
                _staticDataService.GetAdditionalBonus(_type).ApplyBonus(_worldData);
                Destroy(gameObject);
            }
        }

        public void Init(AdditionalBonusType type, Sprite icon)
        {
            _type = type;

            AdditionalBonusConfig config = _staticDataService.GetAdditionalBonus(_type);

            _cost = config.Cost;

            _icon.sprite = icon;
            _countValue.text = config.Count.ToString();
            _costValue.text = _cost.ToString();
        }
    }
}
