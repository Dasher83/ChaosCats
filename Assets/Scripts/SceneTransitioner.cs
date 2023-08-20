using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChaosCats
{
    public class SceneTransitioner : MonoBehaviour
    {
        [SerializeField] private EventBus eventBus;

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            eventBus.LoadSceneWithoutDelay.AddListener(ChangeScene);
            eventBus.LoadSceneWithDelay.AddListener(ChangeScene);
        }

        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void ChangeScene(string sceneName, float transitionTime)
        {
            StartCoroutine(LoadSceneWithTransition(sceneName, transitionTime));
        }

        private IEnumerator LoadSceneWithTransition(string sceneName, float transitionTime)
        {
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(sceneName);
        }
    }
}
