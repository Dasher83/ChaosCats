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

        [SerializeField]
        private TMPro.TextMeshProUGUI ScoreText;

        [SerializeField]
        private HumanAI HumanAI;

        public int playerScore;
        public int levelTime = 60;
        public int timeLeft;
        public int noiseLevel;
        public int frustrationLevel;

        private bool runOver = false;

        private void Start()
        {
            playerScore = 0;
            timeLeft = levelTime;
            noiseLevel = 0;
            frustrationLevel = 0;
        }

        private void Update()
        {
            if (runOver)
                return;

            if (timeLeft == 0)
            {
                Debug.Log("Run is Over, time's up!");
                runOver = true;
                return;
            }
            timeLeft = Mathf.Max(levelTime - (int)Time.timeSinceLevelLoad, 0);
            if (TimerText != null)
                TimerText.text = timeLeft.ToString();
            if (ScoreText != null)
                ScoreText.text = playerScore.ToString();
        }

        public void MakeNoise(Vector3 position) {
            Debug.Log("Noise made at " + position + "!");
            noiseLevel++;
            frustrationLevel++;
            if (HumanAI != null)
                HumanAI.target = position;
        }

        public void updateScore(int durability) {
            playerScore += durability * 10;
        }

    }
}
