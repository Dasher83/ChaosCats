using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosCats
{
    public class ObjectInteract : MonoBehaviour
    {
        public float interactForce = 1000f;

        [SerializeField] private InputActionAsset inputActionAsset;
        [SerializeField] private GameObject smallObject;
        [SerializeField] private Transform forcePoint;

        private bool playerInRange = false;
        private InputActionMap inputActionMap;

        void Start()
        {
            inputActionMap = inputActionAsset.FindActionMap("PlayerActions", true);
            inputActionMap.FindAction("Interact", true).performed += Interact;
        }

        void Update()
        {

        }

        private void OnTriggerEnter(Collider obj) {
            if (obj.CompareTag("Player")) {
                playerInRange = true;
            }
        }

        private void OnTriggerExit(Collider obj) {
            if (obj.CompareTag("Player")) {
                playerInRange = false;
            }
        }

        private void Interact(InputAction.CallbackContext context) {
            if (playerInRange && smallObject != null) {
                Debug.Log("Interactuando small object");
                smallObject.GetComponent<Rigidbody>().AddRelativeForce((forcePoint.position - transform.position) * interactForce);
            }
        }

        void OnDrawGizmos() {
            if (forcePoint != null)
                Gizmos.DrawLine(transform.position, forcePoint.position);
        }
    }
}
