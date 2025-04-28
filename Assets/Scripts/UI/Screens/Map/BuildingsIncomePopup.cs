using System.Collections;
using Assets.Scripts.Data.Map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map
{
    public class BuildingsIncomePopup : MonoBehaviour
    {
        [SerializeField] private GameObject _popupPanel;
        [SerializeField] private TMP_Text _incomeText;
        [SerializeField] private float _displayTime = 3f;
        [SerializeField] private Button _triggerButton;

        private ICurrencyMapData _mapData;

        [Inject]
        public void Construct(ICurrencyMapData mapData)
        {
            _mapData = mapData;
        }

        private void Awake()
        {
            _popupPanel.SetActive(false);
            _triggerButton.onClick.AddListener(OnShowIncomePopup);
        }

        public void OnShowIncomePopup()
        {
            int totalIncome = CalculateTotalIncome();
            _incomeText.text = $"{totalIncome} золота каждые 5 ходов";

            _popupPanel.SetActive(true);
            StartCoroutine(HideAfterDelay());
        }

        private int CalculateTotalIncome()
        {
            int total = 0;
            total = _mapData.GetTotalIncome();

            if (total < 0)
            {
                return 0;
            }

            return total;
        }

        private IEnumerator HideAfterDelay()
        {
            yield return new WaitForSeconds(_displayTime);
            _popupPanel.SetActive(false);
        }

        private void OnDestroy()
        {
            _triggerButton.onClick.RemoveListener(OnShowIncomePopup);
        }
    }
}
