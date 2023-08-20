using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosCats
{
    public class CatInteraction : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActionAsset;

        private SphereCollider interactionCollider;

        private bool isInRange = false;
        private InputActionMap inputActionMap;

        public Animator catAnimator;

        void Start()
        {
            interactionCollider = GetComponentInChildren<SphereCollider>();
            inputActionMap = inputActionAsset.FindActionMap("PlayerActions", true);
            inputActionMap.FindAction("Interact", true).performed += Interact;
            inputActionMap.FindAction("Hide", true).performed += Hide;
            catAnimator = GetComponentInChildren<Animator>();
        }

        void OnTriggerEnter(Collider obj) {
            if (obj.CompareTag("Interactable")) {
                isInRange = true;
                Interactable interactable = obj.GetComponent<Interactable>();
                if (interactable != null)
                    interactable.ShowUI();
            }
        }

        void OnTriggerExit(Collider obj) {
            if (obj.CompareTag("Interactable")) {
                isInRange = false;
                Interactable interactable = obj.GetComponent<Interactable>();
                if (interactable != null)
                    interactable.HideUI();
            }
        }

        private void Interact(InputAction.CallbackContext context) {
            if (isInRange) {
                Collider[] closeColliders = Physics.OverlapSphere(transform.position + interactionCollider.center, interactionCollider.radius);
                Collider[] interactables = Array.FindAll(closeColliders, collider => 
                    collider.CompareTag("Interactable") &&
                     collider.GetComponent<Interactable>().enabled &&
                      collider.GetComponent<Interactable>().interactableObject.canInteract
                );
                Array.Sort(interactables, (x, y) => 
                    Vector3.Distance(x.transform.position, transform.position).CompareTo(
                        Vector3.Distance(y.transform.position, transform.position)
                    )
                );
                if (interactables.Length > 0)
                {
                    interactables[0].GetComponent<Interactable>().Interact();
                    catAnimator.SetTrigger("Attack");
                }                    
            }
        }

        private void Hide(InputAction.CallbackContext context) {
            if (isInRange) {
                Collider[] closeColliders = Physics.OverlapSphere(transform.position + interactionCollider.center, interactionCollider.radius);
                Collider[] interactables = Array.FindAll(closeColliders, collider => 
                    collider.CompareTag("Interactable") &&
                     collider.GetComponent<Interactable>().enabled &&
                      collider.GetComponent<Interactable>().interactableObject.canHide
                );
                Array.Sort(interactables, (x, y) => 
                    Vector3.Distance(x.transform.position, transform.position).CompareTo(
                        Vector3.Distance(y.transform.position, transform.position)
                    )
                );
                if (interactables.Length > 0)
                    interactables[0].GetComponent<Interactable>().Hide();
            }
        }
    }
}
