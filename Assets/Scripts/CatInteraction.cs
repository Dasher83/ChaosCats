using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosCats
{
    public class CatInteraction : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActionAsset;

        private SphereCollider interactionCollider;

        private bool canInteract = false;
        private InputActionMap inputActionMap;

        void Start()
        {
            interactionCollider = GetComponentInChildren<SphereCollider>();
            inputActionMap = inputActionAsset.FindActionMap("PlayerActions", true);
            inputActionMap.FindAction("Interact", true).performed += Interact;
        }

        void OnTriggerEnter(Collider obj) {
            if (obj.CompareTag("Interactable")) {
                canInteract = true;
            }
        }

        void OnTriggerExit(Collider obj) {
            if (obj.CompareTag("Interactable")) {
                canInteract = false;
            }
        }

        private void Interact(InputAction.CallbackContext context) {
            if (canInteract) {
                Collider[] closeColliders = Physics.OverlapSphere(transform.position + interactionCollider.center, interactionCollider.radius);
                Collider[] interactables = Array.FindAll(closeColliders, collider => 
                    collider.CompareTag("Interactable") && collider.GetComponent<Interactable>().enabled
                );
                Array.Sort(interactables, (x, y) => 
                    Vector3.Distance(x.transform.position, transform.position).CompareTo(
                        Vector3.Distance(y.transform.position, transform.position)
                    )
                );
                if (interactables.Length > 0)
                    interactables[0].GetComponent<Interactable>().Interact();
            }
        }
    }
}
