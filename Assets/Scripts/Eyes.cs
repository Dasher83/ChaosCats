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
            if (other.gameObject.CompareTag("Player") && humanAI != null && !GameManager.Instance.catIsHidden)
            {
                Debug.Log("Player detected!");
                Vector3 target = new Vector3(other.gameObject.transform.position.x, 0, other.gameObject.transform.position.z);
                humanAI.target = target;
            }
        }
    }
}
