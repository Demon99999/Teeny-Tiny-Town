using Assets.Scripts.Services.PersistantProgrssService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.GameLogic.Inventory
{
    public class InventoryLock : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _lockIcon;
        [SerializeField] private Button _button;

        [Inject]
        private void Construct(IPersistantProgrss persistentProgressService)
        {
            bool isInventoryUnlocked = persistentProgressService.Progress.StoreData.IsInventoryUnlocked;

            _lockIcon.alpha = isInventoryUnlocked ? 0 : 1;
            _button.interactable = isInventoryUnlocked ? true : false;
        }
    }
}
