using UnityEngine;
using UnityEngine.Events;

namespace ChaosCats.Scriptables
{
    [CreateAssetMenu(fileName = "GameSession", menuName = "ScriptableObjects/GameSession")]
    public class GameSession : ScriptableObject
    {
        private int playerScore;
        public int levelTime;
        public int timeLeft;
        public int noiseLevel;
        public int frustrationLevel;

        public UnityEvent<int> PlayerScored;

        public void Initialize()
        {
            playerScore = 0;
            levelTime = 60;
            timeLeft = levelTime;
            noiseLevel = 0;
            frustrationLevel = 0;
        }

        public void OnEnable()
        {
            PlayerScored.AddListener(
                (scoreToAdd) => { playerScore += scoreToAdd; });
        }

        public void OnDisable()
        {
            PlayerScored.RemoveAllListeners();
        }

        public int PlayerScore => playerScore;
    }
}
