using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ChaosCats
{
    public class SetMainMenuScore : MonoBehaviour
    {
        public CatStatus catStatus;        

        private void Start()
        {
            GetComponent<TextMeshProUGUI>().text = "Score: " + catStatus.playerScore;
        }
    }
}
