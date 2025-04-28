using Assets.Scripts.Data.Map;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI.Screens.Map.Buttons
{
    public class GainButton : ActionHandlerButton
    {
        [SerializeField] private TMP_Text _countValue;

        protected IMapData MapData { get; private set; }

        [Inject]
        private void Construct(IMapData mapData)
        {
            MapData = mapData;
        }

        protected void ChangeCountValue(uint value)
        {
            _countValue.text = value.ToString();
        }
    }
}
