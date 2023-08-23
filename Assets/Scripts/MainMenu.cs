using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaosCats
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private EventBus eventBus;

        private void Start()
        {
            ServiceLocator.Instance.BackgroundMusicPlayer.Play("Main Theme");
        }

        public void Retry()
        {
            Debug.Log("Retry!");
            ServiceLocator.Instance.BackgroundMusicPlayer.StopAll();
            ServiceLocator.Instance.SoundEffectPlayer.Play("meowClick");
            StartCoroutine(WaitForSound());
            eventBus.LoadSceneWithoutDelay?.Invoke("Main v2 (GameDesign)");
        }

        public void Quit()
        {
            Debug.Log("Quit!");
            ServiceLocator.Instance.BackgroundMusicPlayer.StopAll();
            ServiceLocator.Instance.SoundEffectPlayer.Play("meowClick");
            StartCoroutine(WaitForSound());
            Application.Quit();
        }

        IEnumerator WaitForSound()
        {
            yield return new WaitForSeconds(1);
        }
    }
}
