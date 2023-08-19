using UnityEngine;

namespace ChaosCats
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        [SerializeField]
        private TMPro.TextMeshProUGUI TimerText;

        public int playerScore;
        public int levelTime = 60;
        public int timeLeft;
        public int noiseLevel;
        public int frustrationLevel;

        private bool gameOver = false;

        private void Start()
        {
            playerScore = 0;
            timeLeft = levelTime;
            noiseLevel = 0;
            frustrationLevel = 0;
        }

        private void Update()
        {
            if (gameOver)
                return;

            if (timeLeft == 0)
            {
                Debug.Log("Game Over, time's up!");
                gameOver = true;
                return;
            }
            timeLeft = Mathf.Max(levelTime - (int)Time.timeSinceLevelLoad, 0);
            TimerText.text = timeLeft.ToString();
        }
    }
}
