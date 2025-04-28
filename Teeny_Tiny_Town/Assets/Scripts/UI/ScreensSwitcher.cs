using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.Services.StaticDataServices.Configs.Screens;

namespace Assets.Scripts.UI
{
    public class ScreensSwitcher
    {
        private readonly Dictionary<Type, ScreenBase> _screens;

        private ScreenBase _currentScreen;
        private bool _currentScreenHided;

        public ScreensSwitcher()
        {
            _screens = new Dictionary<Type, ScreenBase>();

            _currentScreenHided = false;
        }

        public void RegisterScreen<TScreenBase>(ScreenType screenType, IUIFactory uiFactory)
            where TScreenBase : ScreenBase
        {
            ScreenBase screen = uiFactory.CreateScreen(screenType);

            _screens.Add(typeof(TScreenBase), screen);
        }

        public void Switch<TScreenBase>()
            where TScreenBase : ScreenBase
        {
            if (_currentScreen != null)
            {
                _currentScreen.Hide(callback: () => OpenScren<TScreenBase>());
            }
            else
            {
                OpenScren<TScreenBase>();
            }
        }

        public void Remove<TScreenBase>()
            where TScreenBase : ScreenBase
        {
            ScreenBase screen = _screens[typeof(TScreenBase)];

            if (_currentScreen == screen)
                _currentScreen = null;

            Remove<TScreenBase>(screen);
        }

        public void HideCurrentWindow()
        {
            if (_currentScreenHided || _currentScreen == null)
                return;

            _currentScreenHided = true;

            _currentScreen.Hide(callback: () => _currentScreenHided = false);
        }

        private void Remove<TScreenBase>(ScreenBase screen)
            where TScreenBase : ScreenBase
        {
            screen.Destroy();
            _screens.Remove(typeof(TScreenBase));
        }

        private void OpenScren<TScreenBase>()
            where TScreenBase : ScreenBase
        {
            _currentScreen = _screens[typeof(TScreenBase)];
            _currentScreen.Open();
        }
    }
}
