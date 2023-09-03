using ChaosCats.Scriptables;
using UnityEngine;
using UnityEngine.UI;

namespace ChaosCats
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameSession gameSession;
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

        public GameSession GameSession => gameSession;

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
            player = GameObject.FindGameObjectWithTag("Player");
            gameSession.Initialize();
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

            if (gameSession.timeLeft == 0)
            {
                Debug.Log("Run is Over, time's up!");
                runOver = true;
                ServiceLocator.Instance.BackgroundMusicPlayer.StopAll();
                ServiceLocator.Instance.SoundEffectPlayer.Play("alarm");
                eventBus.LoadSceneWithDelay?.Invoke("MainMenu", 4.0f);

                //return;
            }
            gameSession.timeLeft = Mathf.Max(gameSession.levelTime - (int)Time.timeSinceLevelLoad, 0);
        }

        public void MakeNoise(Vector3 position) {
            Debug.Log("Noise made at " + position + "!");
            gameSession.noiseLevel++;
            gameSession.frustrationLevel++;
            eventBus.MakeNoise?.Invoke();
            if (HumanAI != null)
            {
                HumanAI.target = position;
                HumanAI.OnAlerted();
            }
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
