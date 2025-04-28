using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.GameLogic.SandBox.Action;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Services.PersistantProgrssService;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;
using Zenject;

namespace Assets.Scripts.UI.Screens.SandBox
{
    public class BuildingsPanel : SandboxPanel
    {
        private BuildingPositionHandler _buildingPositionHandler;

        private Dictionary<SandboxPanelElement, BuildingType> _elements;
        private SandboxPanelElement _currentElement;

        [Inject]
        private void Construct(
            IUIFactory uiFactory,
            IPersistantProgrss persistentProgressService,
            BuildingPositionHandler buildingPositionHandler,
            IStaticDataService staticDataService)
        {
            _buildingPositionHandler = buildingPositionHandler;

            _elements = new Dictionary<SandboxPanelElement, BuildingType>();

            foreach (Data.BuildingData buildingData in persistentProgressService.Progress.BuildingDatas)
            {
                BuildingConfig config = staticDataService.GetBuilding<BuildingConfig>(buildingData.Type);

                if (buildingData.IsUnlocked)
                {
                    SandboxPanelElement sandboxPanelElement = uiFactory.CreateSandboxPanelElement(Content, config.IconAssetReference);
                    _elements.Add(sandboxPanelElement, buildingData.Type);

                    sandboxPanelElement.Clicked += OnElementClicked;
                }
                else
                {
                    SandboxPanelElement sandboxPanelElement = uiFactory.CreateSandboxPanelElement(Content, config.LockIconAssetReference);
                    uiFactory.CreateLockIcon(sandboxPanelElement.transform);
                }
            }

            if (_elements.Count > 0)
            {
                _buildingPositionHandler.SetBuilding(_elements.Values.First());
                _currentElement = _elements.Keys.First();
                _currentElement.SetActive(true);
            }
        }

        private void OnDestroy()
        {
            foreach (SandboxPanelElement sandboxPanelElement in _elements.Keys)
                sandboxPanelElement.Clicked -= OnElementClicked;
        }

        private void OnElementClicked(SandboxPanelElement element)
        {
            _currentElement.SetActive(false);
            _currentElement = element;
            _currentElement.SetActive(true);
            _buildingPositionHandler.SetBuilding(_elements[element]);
        }
    }
}