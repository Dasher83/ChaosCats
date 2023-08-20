using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaosCats
{
    [CreateAssetMenu(fileName = "CatStatus", menuName = "ScriptableObjects/CatStatus")]
    public class CatStatus : ScriptableObject
    {
        [SerializeField] private bool isHiding;

        public float playerScore;

        public void initHiding() {
            isHiding = false;
        }

        public void setHiding(bool state) {
            isHiding = state;
        }

        public bool getHiding() {
            return isHiding;
        }
    }
}
