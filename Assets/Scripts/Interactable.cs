using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaosCats
{
    public abstract class Interactable : MonoBehaviour
    {
        public InteractableObject interactableObject;
        virtual public void Interact() {
            if (interactableObject.canInteract) {
                Debug.Log("Interactuando con " + gameObject.name);
            }
        }

        virtual public void Hide() {
            if (interactableObject.canHide) {
                Debug.Log("Escondiendose en " + gameObject.name);
            }
        }

        virtual public void ShowUI() {
            Debug.Log("Mostrando UI de " + gameObject.name);
        }

        virtual public void HideUI() {
            Debug.Log("Escondiendo UI de " + gameObject.name);
        }

    }
}
