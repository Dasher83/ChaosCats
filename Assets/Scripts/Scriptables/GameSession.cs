using UnityEngine;

namespace ChaosCats.Scriptables
{
    [CreateAssetMenu(fileName = "GameSession", menuName = "ScriptableObjects/GameSession")]
    public class GameSession : ScriptableObject
    {
        public int playerScore;
        public int levelTime;
        public int timeLeft;
        public int noiseLevel;
        public int frustrationLevel;

        public void Initialize()
        {
            playerScore = 0;
            levelTime = 60;
            timeLeft = levelTime;
            noiseLevel = 0;
            frustrationLevel = 0;
        }
    }
}
