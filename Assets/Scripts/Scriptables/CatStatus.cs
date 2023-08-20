using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaosCats
{
    [CreateAssetMenu(fileName = "CatStatus", menuName = "ScriptableObjects/CatStatus")]
    public class CatStatus : ScriptableObject
    {
        [SerializeField] private bool isHiding;
        [SerializeField] private EventBus eventBus;

        public int playerScore;

        public void initHiding() {
            isHiding = false;            
        }

        public void setHiding(bool state) {
            isHiding = state;
        }

        public bool getHiding() {
            return isHiding;
        }

        private void OnEnable()
        {
            eventBus.UpdateScriptableScore.AddListener(SetPlayerScore);
        }

        private void SetPlayerScore(int score)
        {
            playerScore = score;
        }
    }
}
