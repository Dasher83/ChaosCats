using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace ChaosCats
{
    public class HumanAI : MonoBehaviour
    {
        [SerializeField]
        private Transform Home;

        [SerializeField]
        private CatStatus catStatus;

        public Vector3? target = null;

        private NavMeshAgent agent;
        private Animator animator;
        private GameObject[] interactableObjects;

        void Start()
        {
            interactableObjects = GameObject.FindGameObjectsWithTag("Interactable");
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();
        }

        void Update()
        {
            if (GameManager.Instance.runOver)
            {
                agent.SetDestination(agent.transform.position);
                animator.SetFloat("Speed", 0);
                Destroy(this);
                return;
            }

            if (animator != null)
                animator.SetFloat("Speed", agent.velocity.magnitude);

            if (target == null)
            {
                return;
            }

            float distanceTargetDestination = Vector3.Distance(agent.destination, (Vector3)target);

            if (distanceTargetDestination > 0.2f)
            {
                // Debug.Log("Destination: " + agent.destination + " || Target: " + (Vector3)target + " || Distance: " + distanceTargetDestination);
                agent.SetDestination((Vector3)target);
            }
            else
            {
                if (agent.remainingDistance <= agent.stoppingDistance || catStatus.getHiding())
                {
                    target = null;
                    int randomIndex = Random.Range(0, interactableObjects.Length);
                    agent.SetDestination(interactableObjects[randomIndex].transform.position);
                    StartCoroutine(WaitAndGoHome());
                }
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (target != null)
                Gizmos.DrawLine(transform.position, (Vector3)target);

            if (agent != null && agent.destination != null)
                Gizmos.DrawCube(agent.destination, Vector3.one);
        }

        IEnumerator WaitAndGoHome()
        {
            yield return new WaitForSeconds(5);
            agent.SetDestination(Home.transform.position);
        }
    }
}