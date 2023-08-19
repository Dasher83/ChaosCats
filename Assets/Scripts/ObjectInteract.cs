using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosCats
{
    public class ObjectInteract : Interactable
    {
        public float interactForce = 1000f;

        [SerializeField] private GameObject smallObject;
        [SerializeField] private Transform forcePoint;

        override public void Interact() {
            if (smallObject != null) {
                Debug.Log("Interactuando con small object");
                smallObject.GetComponent<Rigidbody>().AddRelativeForce((forcePoint.position - transform.position) * interactForce);
            }
        }

        void OnDrawGizmos() {
            if (forcePoint != null)
                Gizmos.DrawLine(transform.position, forcePoint.position);
        }
    }
}
