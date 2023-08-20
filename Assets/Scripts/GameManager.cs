using UnityEngine;
using UnityEngine.UI;

namespace ChaosCats
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private EventBus eventBus;

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

        [SerializeField]
        private Image overlayImage;

        [SerializeField]
        private Renderer playerRenderer;

        [SerializeField] 
        private Material transparentMaterial;

        public int playerScore;
        public int levelTime = 60;
        public int timeLeft;
        public int noiseLevel;
        public int frustrationLevel;

        private bool _catIsHidden = false;
        public bool catIsHidden {
            get { return _catIsHidden; }
            set {
                _catIsHidden = value;
                isDarkening = value;
                if (value) {
                    currentAlpha = overlayImage.color.a;
                    playerRenderer.material = transparentMaterial;
                } else {
                    currentAlpha = 0f;
                    Color overlayColor = overlayImage.color;
                    overlayColor.a = currentAlpha;
                    overlayImage.color = overlayColor;
                    playerRenderer.material = originalMaterial;
                }
            }
        }

        // Darkening screen
        private bool isDarkening = false;
        private float darkenSpeed = 9.0f;
        private float targetAlpha = 0.8f;
        private float currentAlpha = 0f;

        private Material originalMaterial;

        private bool runOver = false;

        private void Start()
        {
            playerScore = 0;
            timeLeft = levelTime;
            noiseLevel = 0;
            frustrationLevel = 0;
            originalMaterial = playerRenderer.material;
        }

        private void Update()
        {
            if (isDarkening)
            {
                currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, darkenSpeed * Time.deltaTime);
                Color overlayColor = overlayImage.color;
                overlayColor.a = currentAlpha;
                overlayImage.color = overlayColor;

                if (currentAlpha >= targetAlpha) {
                    isDarkening = false;
                }
            }
            
            if (runOver)
                return;

            if (timeLeft == 0)
            {
                Debug.Log("Run is Over, time's up!");
                runOver = true;
                ServiceLocator.Instance.BackgroundMusicPlayer.StopAll();
                eventBus.LoadSceneWithoutDelay?.Invoke("MainMenu");
                //return;
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
            eventBus.MakeNoise?.Invoke();
            if (HumanAI != null)
                HumanAI.target = position;
        }

        public void UpdateScore(int pointsToAdd) {
            playerScore += pointsToAdd;
        }

    }
}
