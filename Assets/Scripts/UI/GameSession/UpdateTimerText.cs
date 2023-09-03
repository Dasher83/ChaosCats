using TMPro;
using UnityEngine;

namespace ChaosCats.UI.GameSession
{
    public class UpdateTimerText : MonoBehaviour
    {
        private Scriptables.GameSession gameSession;
        private TextMeshProUGUI timerText;

        private void Start()
        {
            gameSession = Resources.Load<Scriptables.GameSession>("GameSession");
            timerText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            timerText.text = gameSession.timeLeft.ToString();
        }
    }
}
