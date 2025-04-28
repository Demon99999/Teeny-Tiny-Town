using UnityEngine;
using Zenject;

namespace Assets.Scripts.Audio
{
    public class UiSoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _auidoSource;

        public void Play()
        {
            _auidoSource.Play();
        }

        public class Factory : PlaceholderFactory<UiSoundPlayer>
        {

        }
    }
}
