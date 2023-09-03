using UnityEngine;

namespace ChaosCats
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private EventBus eventBus;

        private void Start()
        {
            eventBus.PlayerCaught.AddListener(EndGame);
        }

        private void EndGame()
        {
            Debug.ClearDeveloperConsole();
            Debug.Log("Game Over");
            ServiceLocator.Instance.BackgroundMusicPlayer.StopAll();
            eventBus.UpdateScriptableScore?.Invoke(GameManager.Instance.GameSession.playerScore);
            eventBus.LoadSceneWithoutDelay?.Invoke("MainMenu");
        }
    }
}
