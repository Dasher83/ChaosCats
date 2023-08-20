using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaosCats
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private EventBus eventBus;

        public void Retry()
        {
            Debug.Log("Retry!");
            eventBus.LoadSceneWithoutDelay?.Invoke("Main");
        }

        public void Quit()
        {
            Debug.Log("Quit!");
            Application.Quit();
        }
    }
}
