using Assets.Scripts.Data.Map;
using MPUIKIT;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map.Panels
{
    public class GoalPanel : MonoBehaviour
    {
        private const string GoalsOveredInfo = "Все цели достигнуты";

        [SerializeField] private MPImage _fill;
        [SerializeField] private TMP_Text _progressValue;
        [SerializeField] private TMP_Text _goalValue;

        private PointsData _pointsData;

        [Inject]
        private void Construct(IMapData worldData)
        {
            _pointsData = worldData.PointsData;

            _pointsData.Scorred += OnPointsScorred;
            _pointsData.GoalAchieved += OnGoalAchieved;
            _pointsData.GoalsOvered += OnGoalsOvered;

            OnGoalAchieved();
            OnPointsScorred();
        }

        private void OnDestroy()
        {
            _pointsData.Scorred -= OnPointsScorred;
            _pointsData.GoalAchieved -= OnGoalAchieved;
            _pointsData.GoalsOvered -= OnGoalsOvered;
        }

        private void OnGoalsOvered()
        {
            _fill.fillAmount = 1;
            _progressValue.text = string.Empty;
            _goalValue.text = GoalsOveredInfo;
        }

        private void OnGoalAchieved()
        {
            if (_pointsData.IsGoalsOvered == false)
                _goalValue.text = GetGoalValue();
        }

        private void OnPointsScorred()
        {
            if (_pointsData.IsGoalsOvered)
                return;

            _progressValue.text = GetProgressValue();
            _fill.fillAmount = GetFill();
        }

        private string GetProgressValue()
        {
            return $"{(int)((float)_pointsData.PointsCount / _pointsData.Goal * 100f)}%";
        }

        private string GetGoalValue()
        {
            return $"Цель {_pointsData.Goal} очков";
        }

        private float GetFill()
        {
            return (float)_pointsData.PointsCount / _pointsData.Goal;
        }
    }
}
