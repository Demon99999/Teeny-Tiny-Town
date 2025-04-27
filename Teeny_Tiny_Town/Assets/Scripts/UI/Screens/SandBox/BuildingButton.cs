using Assets.Scripts.GameLogic.SandBox.Action;
using Zenject;

namespace Assets.Scripts.UI.Screens.SandBox
{
    public class BuildingButton : ActionHandlerButton
    {
        private BuildingPositionHandler _buildingPositionHandler;

        [Inject]
        private void Construct(BuildingPositionHandler buildingPositionHandler)
        {
            _buildingPositionHandler = buildingPositionHandler;

            _buildingPositionHandler.Entered += OnBuildingPositionHandlerEntered;
            _buildingPositionHandler.Exited += OnBuildingPositionHandlerExited;
        }

        private void OnDestroy()
        {
            _buildingPositionHandler.Entered -= OnBuildingPositionHandlerEntered;
            _buildingPositionHandler.Exited -= OnBuildingPositionHandlerExited;
        }

        private void OnBuildingPositionHandlerExited()
        {
            SetActive(false);
        }

        private void OnBuildingPositionHandlerEntered()
        {
            SetActive(true);
        }
    }
}
