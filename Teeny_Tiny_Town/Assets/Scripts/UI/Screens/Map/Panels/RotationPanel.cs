using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map.Panels
{
    public class RotationPanel : MonoBehaviour
    {
        [SerializeField] private Button _rotateWorldСlockwiseButton;
        [SerializeField] private Button _rotateWorldСounterclockwiseButton;

        private GameLogic.Map.Map _world;

        [Inject]
        private void Construct(GameLogic.Map.Map world)
        {
            _world = world;

            _rotateWorldСlockwiseButton.onClick.AddListener(OnRotateWorldClockwiseButtonClicked);
            _rotateWorldСounterclockwiseButton.onClick.AddListener(OnRotateWorldCounterclockwiseButtonClicked);
        }

        private void OnDestroy()
        {
            _rotateWorldСlockwiseButton.onClick.RemoveListener(OnRotateWorldClockwiseButtonClicked);
            _rotateWorldСounterclockwiseButton.onClick.RemoveListener(OnRotateWorldCounterclockwiseButtonClicked);
        }

        private void OnRotateWorldCounterclockwiseButtonClicked()
        {
            _world.RotateСounterclockwise();
        }

        private void OnRotateWorldClockwiseButtonClicked()
        {
            _world.RotateСlockwise();
        }

        public class Factory : PlaceholderFactory<RotationPanel> { }
    }
}
