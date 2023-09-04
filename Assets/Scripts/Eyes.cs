using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaosCats
{
    public class Eyes : MonoBehaviour
    {
        private HumanAI humanAI;

        private void Start()
        {
            humanAI = GetComponentInParent<HumanAI>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && humanAI != null && !GameManager.Instance.catIsHidden)
            {
                Debug.Log("Player detected!");
                humanAI.OnAlerted();
            }
        }

        void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && humanAI != null && !GameManager.Instance.catIsHidden)
            {
                humanAI.target = other.gameObject.transform.position;
            }
        }
    }
}
