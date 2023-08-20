using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaosCats
{
    public class Eyes : MonoBehaviour
    {
        [SerializeField]
        private HumanAI humanAI;

        void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && humanAI != null)
            {
                Debug.Log("Player detected!");
                humanAI.target = other.gameObject.transform.position;
            }
        }
    }
}
