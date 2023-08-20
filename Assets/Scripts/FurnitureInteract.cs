using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
        [SerializeField] private Image overlayImage;
        [SerializeField] private float inRangeDistance;
        [SerializeField] private int durability;
        [SerializeField] private ParticleSystem smokeParticleSystem;

        // Darkening screen
        private float darkenSpeed = 9.0f;
        private bool isDarkening = false;
        private float targetAlpha = 0.8f;
        private float currentAlpha = 0f;

        // Interaction bools
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
            smokeParticleSystem.Stop();

            objetoRoto.SetActive(false);
        }

        private void Update() {
            if (isDarkening)
            {
                currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, darkenSpeed * Time.deltaTime);
                Color overlayColor = overlayImage.color;
                overlayColor.a = currentAlpha;
                overlayImage.color = overlayColor;

                if (currentAlpha >= targetAlpha) {
                    isDarkening = false;
                }
            }

            Debug.Log(playerInRange);
        }

        private void FixedUpdate()
        {
            CheckPlayerInRange();
        }

        private void CheckPlayerInRange()
        {
            Vector3 distanceToPlayer = (player.transform.position - transform.position);

            playerInRange = distanceToPlayer.magnitude < inRangeDistance;
        }

        private void Interact(InputAction.CallbackContext context) {
            if (playerInRange && !isBroken) {
                if (durability > 0) {
                    durability--;
                } else {
                    CambiarModelo();
                    smokeParticleSystem.Play();
                    isBroken = true;
                }
            }
        }

        private void Hide(InputAction.CallbackContext context) {
            if (!playerInRange) {
                return;
            }
            if (!catStatus.getHiding()) {
                //Debug.Log("Esconderse");
                catStatus.setHiding(true);
                StartDarkenEffect();
                SetTransparencia();
            } else {
                //Debug.Log("No esconderse");
                catStatus.setHiding(false);
                StopDarkenEffect();
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

        public void StartDarkenEffect() {
            isDarkening = true;
            currentAlpha = overlayImage.color.a;
        } 

        public void StopDarkenEffect() {
            isDarkening = false;
            currentAlpha = 0f;
            Color overlayColor = overlayImage.color;
            overlayColor.a = currentAlpha;
            overlayImage.color = overlayColor;
        }
    }
}
