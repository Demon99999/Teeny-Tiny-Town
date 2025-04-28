using Assets.Scripts.GameLogic.Map.Representation.ActionHandler;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map.Buttons
{
    public class ReplaceBuildingButton : GainButton
    {
        private ReplacedBuildingPositionHandler _replacedBuildingPositionHandler;

        [Inject]
        private void Construct(ReplacedBuildingPositionHandler replacedBuildingPositionHandler)
        {
            _replacedBuildingPositionHandler = replacedBuildingPositionHandler;

            ChangeCountValue(MapData.ReplaceItems.Count);

            _replacedBuildingPositionHandler.Entered += OnReplaceBuildingPositionHandlerEntered;
            _replacedBuildingPositionHandler.Exited += OnReplaceBuildingPositionHandlerExited;
            MapData.ReplaceItems.CountChanged += ChangeCountValue;
        }

        private void OnDestroy()
        {
            _replacedBuildingPositionHandler.Entered -= OnReplaceBuildingPositionHandlerEntered;
            _replacedBuildingPositionHandler.Exited -= OnReplaceBuildingPositionHandlerExited;
            MapData.BulldozerItems.CountChanged -= ChangeCountValue;
        }

        private void OnReplaceBuildingPositionHandlerExited()
        {
            SetActive(false);
        }

        private void OnReplaceBuildingPositionHandlerEntered()
        {
            SetActive(true);
        }
    }
}
