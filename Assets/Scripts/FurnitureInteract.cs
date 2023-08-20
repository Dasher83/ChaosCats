using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ChaosCats
{
    public class FurnitureInteract : Interactable
    {
        public int damagePointsValue;
        public int breakingPointsValue;
        [SerializeField] private GameObject objetoIntacto;
        [SerializeField] private GameObject objetoRoto;
        [SerializeField] private int durability;
        [SerializeField] private ParticleSystem smokeParticleSystem;
        [SerializeField] private Material transparentMaterial;
        [SerializeField] private Transform npcWaypoint;
        [SerializeField] private AudioClip breakSound;
        [SerializeField] private AudioClip hideSound;

        [Header("Iconos de Interaccion")]
        [SerializeField] private GameObject UIInteraction;
        public GameObject[] UIInteractables;
       
        private int currentDurability;
        private bool isBroken = false;


        void Start()
        {
            smokeParticleSystem.Stop();
            currentDurability = durability;

            UIInteraction.SetActive(false);
            objetoRoto.SetActive(false);
            InteractTypeCheck();
        }

        override public void ShowUI() {
            if (isBroken) return;
            UIInteraction.SetActive(true);
        }

        override public void HideUI() {
            UIInteraction.SetActive(false);
        }

        /*
        private void FixedUpdate()
        {
            CheckPlayerInRange();
        }*/

        /*private void CheckPlayerInRange()
        {
            Vector3 distanceToPlayer = (player.transform.position - transform.position);

            playerInRange = distanceToPlayer.magnitude < inRangeDistance;
        }*/

        override public void Interact() {
            if (!isBroken && !GameManager.Instance.catIsHidden) {
                if (npcWaypoint != null) {
                    GameManager.Instance.MakeNoise(npcWaypoint.position);
                } else {
                    GameManager.Instance.MakeNoise(transform.position);
                }
                smokeParticleSystem.Play();
                if (currentDurability > 0) {
                    currentDurability--;
                    GameManager.Instance.UpdateScore(damagePointsValue);
                } else {
                    if (breakSound != null) {
                        AudioSource.PlayClipAtPoint(breakSound, transform.position);
                    }
                    CambiarModelo();
                    GameManager.Instance.UpdateScore(breakingPointsValue);
                    isBroken = true;
                }
            }
        }

        override public void Hide() {
            if (isBroken) return;

            if (!GameManager.Instance.catIsHidden) {
                //Debug.Log("Esconderse");
                if (breakSound != null) {
                    AudioSource.PlayClipAtPoint(hideSound, transform.position);
                }
                GameManager.Instance.catIsHidden = true;
                HideUI();
            } else {
                //Debug.Log("No esconderse");
                GameManager.Instance.catIsHidden = false;
                ShowUI();
            }
        }

        private void CambiarModelo() {
            objetoIntacto.SetActive(false);
            objetoRoto.SetActive(true);
            HideUI();
        }

        private void InteractTypeCheck()
        {
            if (UIInteractables[0] != null) UIInteractables[0].SetActive(interactableObject.canHide);
            if (UIInteractables[1] != null) UIInteractables[1].SetActive(interactableObject.canInteract);
        }
    }
}
