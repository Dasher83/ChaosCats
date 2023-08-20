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
        [SerializeField] private GameManager gameManager;
        [SerializeField] private Material transparentMaterial;

        // Darkening screen
        private float darkenSpeed = 9.0f;
        private bool isDarkening = false;
        private float targetAlpha = 0.8f;
        private float currentAlpha = 0f;

        // Interaction bools
        private int currentDurability;
        private bool isBroken = false;
        private bool playerInRange = false;
        private GameObject playerModel;
        private GameObject player;
        private Renderer playerRenderer;
        private Material originalMaterial;
        private InputActionMap inputActionMap;

        void Start()
        {
            inputActionMap = inputActionAsset.FindActionMap("PlayerActions", true);
            inputActionMap.FindAction("Interact", true).performed += Interact;
            inputActionMap.FindAction("Hide", true).performed += Hide;
            player = GameObject.FindWithTag("Player");
            playerModel = GameObject.FindWithTag("PlayerModel");
            playerRenderer = playerModel.GetComponent<Renderer>();
            originalMaterial = playerRenderer.material;
            smokeParticleSystem.Stop();
            currentDurability = durability;

            objetoRoto.SetActive(false);
        }

        private void Update() {
            CheckPlayerInRange();

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
        }
/*
        private void FixedUpdate()
        {
            CheckPlayerInRange();
        }*/

        private void CheckPlayerInRange()
        {
            Vector3 distanceToPlayer = (player.transform.position - transform.position);

            playerInRange = distanceToPlayer.magnitude < inRangeDistance;
        }

        private void Interact(InputAction.CallbackContext context) {
            if (playerInRange && !isBroken) {
                if (currentDurability > 0) {
                    currentDurability--;
                } else {
                    CambiarModelo();
                    smokeParticleSystem.Play();
                    gameManager.updateScore(durability);
                    isBroken = true;
                }
            }
        }

        private void Hide(InputAction.CallbackContext context) {
            if (!playerInRange) {
                return;
            }
            Debug.Log($"playerInRange: {playerInRange}");
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
                playerRenderer.material = transparentMaterial;
            } else {
                playerRenderer.material = originalMaterial;
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
