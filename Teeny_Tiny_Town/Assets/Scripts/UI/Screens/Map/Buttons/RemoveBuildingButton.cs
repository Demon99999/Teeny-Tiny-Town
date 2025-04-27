using Assets.Scripts.GameLogic.Map.Representation.ActionHandler;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map.Buttons
{
    public class RemoveBuildingButton : GainButton
    {
        private RemovedBuildingPositionHandler _removedBuildingPositionHandler;

        [Inject]
        private void Construct(RemovedBuildingPositionHandler removedBuildingPositionHandler)
        {
            _removedBuildingPositionHandler = removedBuildingPositionHandler;

            ChangeCountValue(MapData.BulldozerItems.Count);
            
            _removedBuildingPositionHandler.Entered += OnRemovedBuildingPositionHandlerEntered;
            _removedBuildingPositionHandler.Exited += OnRemovedBuildingPositionHandlerExited;
            MapData.BulldozerItems.CountChanged += ChangeCountValue;
        }

        private void OnDestroy()
        {
            _removedBuildingPositionHandler.Entered -= OnRemovedBuildingPositionHandlerEntered;
            _removedBuildingPositionHandler.Exited -= OnRemovedBuildingPositionHandlerExited;
            MapData.BulldozerItems.CountChanged -= ChangeCountValue;
        }

        private void OnRemovedBuildingPositionHandlerExited()
        {
            SetActive(false);
        }

        private void OnRemovedBuildingPositionHandlerEntered()
        {
            SetActive(true);
        }
    }
}
