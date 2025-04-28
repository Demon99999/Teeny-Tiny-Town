using System.Collections.Generic;
using Assets.Scripts.GameLogic.Map;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs.Maps;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI.Screens.SelectionMap
{
    public class PeculiarityPanel : MonoBehaviour
    {
        private IStaticDataService _staticDataService;
        private IUIFactory _uiFactory;
        private MapSwitcher _mapSwitcher;
        private List<PeculiarityIconPanel> _icons;

        [Inject]
        private void Construct(
            IStaticDataService staticDataService,
            IUIFactory uiFactory,
            MapSwitcher mapSwitcher)
        {
            _staticDataService = staticDataService;
            _uiFactory = uiFactory;
            _mapSwitcher = mapSwitcher;

            _icons = new List<PeculiarityIconPanel>();

            ChangeIcons(_mapSwitcher.CurrentWorldDataId);

            _mapSwitcher.CurrentWorldChanged += ChangeIcons;
        }

        private void OnDestroy()
        {
            _mapSwitcher.CurrentWorldChanged -= ChangeIcons;
        }

        private void ChangeIcons(string worldDataId)
        {
            foreach (PeculiarityIconPanel panel in _icons)
            {
                Destroy(panel.gameObject);
            }

            _icons.Clear();

            MapConfig worldConfig = _staticDataService.GetMap<MapConfig>(worldDataId);

            foreach (Sprite iconAssetReference in worldConfig.PeculiarityIcon)
            {
                _icons.Add(_uiFactory.CreatePeculiarityIconPanel(iconAssetReference, transform));
            }
        }
    }
}