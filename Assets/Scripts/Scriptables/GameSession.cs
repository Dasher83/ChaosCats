using UnityEngine;
using UnityEngine.Events;

namespace ChaosCats.Scriptables
{
    [CreateAssetMenu(fileName = "GameSession", menuName = "ScriptableObjects/GameSession")]
    public class GameSession : ScriptableObject
    {
        private int playerScore;
        private float totalSessionDuration;
        private float sessionTimeLeft;
        public int noiseLevel;
        public int frustrationLevel;

        public UnityEvent<int> PlayerScored;

        public void Initialize()
        {
            playerScore = 0;
            totalSessionDuration = 60;
            sessionTimeLeft = totalSessionDuration;
            noiseLevel = 0;
            frustrationLevel = 0;
        }

        public void Tick(float deltaTime)
        {
            sessionTimeLeft -= deltaTime;
            if (sessionTimeLeft < 0) sessionTimeLeft = 0;
        }

        public void OnEnable()
        {
            PlayerScored.AddListener(AddToPlayerScore);
        }

        public void OnDisable()
        {
            PlayerScored?.RemoveListener(AddToPlayerScore);
        }

        public int PlayerScore => playerScore;
        public float TotalSessionDuration => TotalSessionDuration;
        public float SessionTimeLeft => sessionTimeLeft;

        private void AddToPlayerScore(int scoreToAdd) => playerScore += scoreToAdd;
    }
}
