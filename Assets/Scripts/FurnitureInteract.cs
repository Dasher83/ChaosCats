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
        [SerializeField] private GameObject objetoIntacto;
        [SerializeField] private GameObject objetoRoto;
        [SerializeField] private CatStatus catStatus;

        private bool isBroken = false;
        private bool isHiding = false;
        private bool playerInRange = false;
        private GameObject player;
        private Renderer playerRenderer;
        private Color originalColor;
        private InputActionMap inputActionMap;

        void Start()
        {
            inputActionMap = inputActionAsset.FindActionMap("PlayerActions", true);
            inputActionMap.FindAction("Interact", true).performed += Interact;
            inputActionMap.FindAction("Hide", true).performed += Hide;
            player = GameObject.FindWithTag("Player");
            playerRenderer = player.GetComponent<Renderer>();
            originalColor = playerRenderer.material.color;

            objetoRoto.SetActive(false);
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
                CambiarModelo();
                isBroken = true;
            }
        }

        private void Hide(InputAction.CallbackContext context) {
            if (!playerInRange) {
                return;
            }
            if (!catStatus.getHiding()) {
                //Debug.Log("Esconderse");
                catStatus.setHiding(true);
                SetTransparencia();
            } else {
                //Debug.Log("No esconderse");
                catStatus.setHiding(false);
                SetTransparencia();
            }
        }

        private void CambiarModelo() {
            if (!isBroken) {
                objetoIntacto.SetActive(false);
                objetoRoto.SetActive(true);
            }
        }

        private void SetTransparencia() {
            if (catStatus.getHiding()) {
                Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.0f);
                playerRenderer.material.color = newColor;
            } else {
                playerRenderer.material.color = originalColor;
            }
        }
    }
}
