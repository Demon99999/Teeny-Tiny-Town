using Assets.Scripts.Services.StaticDataServices;
using Assets.Scripts.Services.StaticDataServices.Configs;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
    public abstract class Panel : MonoBehaviour
    {
        protected AnimationsConfig AnimationsConfig { get; private set; }

        [Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            AnimationsConfig = staticDataService.AnimationsConfig;
        }

        public abstract void Open();
        public virtual void Hide() { }
    }
}
