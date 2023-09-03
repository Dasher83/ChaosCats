using ChaosCats.Scriptables;
using TMPro;
using UnityEngine;

namespace ChaosCats.UI
{
    public class UpdateTimerText : MonoBehaviour
    {
        private GameSession gameSession;
        private TextMeshProUGUI timerText;

        private void Start()
        {
            gameSession = Resources.Load<GameSession>("GameSession");
            timerText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            timerText.text = gameSession.timeLeft.ToString();
        }
    }
}
