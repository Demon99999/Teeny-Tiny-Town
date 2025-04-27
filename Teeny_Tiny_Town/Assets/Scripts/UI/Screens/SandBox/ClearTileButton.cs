using Assets.Scripts.GameLogic.SandBox.Action;
using Zenject;

namespace Assets.Scripts.UI.Screens.SandBox
{
    public class ClearTileButton : ActionHandlerButton
    {
        private ClearTilePositionHandler _clearTilePositionHandler;

        [Inject]
        private void Construct(ClearTilePositionHandler clearTilePositionHandler)
        {
            _clearTilePositionHandler = clearTilePositionHandler;

            _clearTilePositionHandler.Entered += OnClearTilePositionHandlerEntered;
            _clearTilePositionHandler.Exited += OnClearTilePositionHandlerExited;
        }

        private void OnDestroy()
        {
            _clearTilePositionHandler.Entered -= OnClearTilePositionHandlerEntered;
            _clearTilePositionHandler.Exited -= OnClearTilePositionHandlerExited;
        }

        private void OnClearTilePositionHandlerExited()
        {
            SetActive(false);
        }

        private void OnClearTilePositionHandlerEntered()
        {
            SetActive(true);
        }
    }
}
