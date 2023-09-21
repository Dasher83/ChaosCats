using TMPro;
using UnityEngine;

namespace ChaosCats.UI.GameSession
{
    public class UpdateScoreText : MonoBehaviour
    {
        private const string InitialScoreText = "0";

        private Scriptables.GameSession gameSession;
        private TextMeshProUGUI scoreText;

        private void Start()
        {
            gameSession = Resources.Load<Scriptables.GameSession>("GameSession");
            scoreText = GetComponent<TextMeshProUGUI>();
            scoreText.text = InitialScoreText;
            gameSession.PlayerScored.AddListener(SetScoreText);
        }

        private void SetScoreText(int score) => scoreText.text = gameSession.PlayerScore.ToString();

        public void OnDisable()
        {
            if (gameSession == null) return;

            gameSession.PlayerScored?.RemoveListener(SetScoreText);
        }
    }
}
