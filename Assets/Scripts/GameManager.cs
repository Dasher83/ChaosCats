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
        private Sprite angryIcon;

        [SerializeField]
        private Sprite sleepIcon;

        [SerializeField]
        private GameObject humanStateUI;

        [SerializeField]
        private AudioClip alarmSound;

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
                player.GetComponent<CatMovement>().ToggleHiding(value);
                if (value) { // Hide
                    currentAlpha = overlayImage.color.a;
                } else { // Unhide
                    currentAlpha = 0f;
                    Color overlayColor = overlayImage.color;
                    overlayColor.a = currentAlpha;
                    overlayImage.color = overlayColor;
                }
            }
        }

        // Darkening screen
        private bool isDarkening = false;
        private float darkenSpeed = 9.0f;
        private float targetAlpha = 0.8f;
        private float currentAlpha = 0f;
        
        private GameObject player;

        public bool runOver = false;

        private void Start()
        {
            playerScore = 0;
            timeLeft = levelTime;
            noiseLevel = 0;
            frustrationLevel = 0;
            player = GameObject.FindGameObjectWithTag("Player");
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
                ServiceLocator.Instance.SoundEffectPlayer.Play("alarm");
                eventBus.UpdateScriptableScore?.Invoke(playerScore);
                eventBus.LoadSceneWithDelay?.Invoke("MainMenu", 4.0f);

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
            {
                // Normalize Y to 0 before setting target
                position.y = 0;
                HumanAI.target = position;
            }
        }

        public void UpdateScore(int pointsToAdd) {
            playerScore += pointsToAdd;
        }

        public void UpdateHumanStateUI(bool humanAwakened) {
            if (humanAwakened) {
                humanStateUI.GetComponent<Image>().sprite = angryIcon;
            } else {
                humanStateUI.GetComponent<Image>().sprite = sleepIcon;
            }
            // TODO: Play UI animation
            // humanStateUI.GetComponent<Animator>().Play("UIPop");
        }

    }
}
