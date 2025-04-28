using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Screens.SelectionMap
{
    public class PeculiarityIconPanel : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        public void Init(Sprite icon)
        {
            _icon.sprite = icon;
        }
    }
}