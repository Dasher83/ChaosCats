using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosCats
{
    public class FurnitureInteract : MonoBehaviour
    {
        [SerializeField] private bool canHide;
        [SerializeField] private bool canDestroy;
        [SerializeField] private InputActionAsset inputActionAsset;

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
            if (playerInRange) {
                Debug.Log("Interactuando");
            }
        }
    }
}
