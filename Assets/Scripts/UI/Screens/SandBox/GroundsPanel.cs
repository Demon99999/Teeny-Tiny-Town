using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.GameLogic.SandBox.Action;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps.SandBox;
using Zenject;

namespace Assets.Scripts.UI.Screens.SandBox
{
    public class GroundsPanel : SandboxPanel
    {
        private GroundPositionHandler _groundPositionHaneler;

        private Dictionary<SandboxPanelElement, SandboxGroundType> _elements;
        private SandboxPanelElement _currentElement;

        [Inject]
        private void Construct(IUIFactory uiFactory, IStaticDataService staticDataService, GroundPositionHandler groundPositionHandler)
        {
            _groundPositionHaneler = groundPositionHandler;

            _elements = new Dictionary<SandboxPanelElement, SandboxGroundType>();

            foreach (SandboxGroundConfig groundConfig in staticDataService.SandboxConfig.Grounds)
            {
                SandboxPanelElement sandboxPanelElement = uiFactory.CreateSandboxPanelElement(Content, groundConfig.Icon);
                _elements.Add(sandboxPanelElement, groundConfig.Type);

                sandboxPanelElement.Clicked += OnElementClicked;
            }

            _groundPositionHaneler.SetGround(_elements.Values.First());
            _currentElement = _elements.Keys.First();

            _currentElement.SetActive(true);
        }

        private void OnDestroy()
        {
            foreach (SandboxPanelElement sandboxPanelElement in _elements.Keys)
            {
                sandboxPanelElement.Clicked -= OnElementClicked;
            }
        }

        private void OnElementClicked(SandboxPanelElement element)
        {
            _currentElement.SetActive(false);
            _currentElement = element;
            _currentElement.SetActive(true);
            _groundPositionHaneler.SetGround(_elements[element]);
        }
    }
}
