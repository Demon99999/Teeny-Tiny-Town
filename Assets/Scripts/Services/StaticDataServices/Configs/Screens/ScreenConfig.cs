using System;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs.Screens
{
    [Serializable]
    public class ScreenConfig
    {
        [SerializeField] private ScreenBase _template;
        [SerializeField] private ScreenType _type;

        public ScreenBase Template => _template;

        public ScreenType Type => _type;
    }
}