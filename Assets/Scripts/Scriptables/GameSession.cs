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
        private int noiseLevel;
        public int frustrationLevel;

        public UnityEvent<int> PlayerScored;
        public UnityEvent MadeNoise;

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
            MadeNoise.AddListener(AddToNoiseLevel);
        }

        public void OnDisable()
        {
            PlayerScored?.RemoveListener(AddToPlayerScore);
            MadeNoise?.RemoveListener(AddToNoiseLevel);
        }

        public int PlayerScore => playerScore;
        public float TotalSessionDuration => TotalSessionDuration;
        public float SessionTimeLeft => sessionTimeLeft;
        public int NoiseLevel => noiseLevel;

        private void AddToPlayerScore(int scoreToAdd) => playerScore += scoreToAdd;
        private void AddToNoiseLevel() => noiseLevel++;
    }
}
