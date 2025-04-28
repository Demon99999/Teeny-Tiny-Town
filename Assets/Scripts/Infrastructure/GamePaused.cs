using UnityEngine;
using YG;

namespace Assets.Scripts.Infrastructure
{
    public class GamePaused : MonoBehaviour
    {
        private void OnEnable()
        {
            YandexGame.onVisibilityWindowGame += OnVisibilityWindowGame;
        }

        private void OnDisable()
        {
            YandexGame.onVisibilityWindowGame -= OnVisibilityWindowGame;
        }

        private void OnApplicationPause(bool isPaused)
        {
            if (isPaused)
            {
                ChangeState(true, 0);
            }
            else
            {
                ChangeState(false, 1);
            }
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                ChangeState(true, 0);
            }
            else
            {
                ChangeState(false, 1);
            }
        }

        private void OnVisibilityWindowGame(bool visible)
        {
            if (visible)
            {
                ChangeState(false, 1);
            }
            else
            {
                ChangeState(true, 0);
            }
        }

        private void ChangeState(bool isValue, int value)
        {
            Time.timeScale = value;
            AudioListener.pause = isValue;
        }
    }
}
