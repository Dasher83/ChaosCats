using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
            if (animator != null)
                animator.SetFloat("Speed", agent.velocity.magnitude);

            if (target == null)
            {
                return;
            }

            float distanceTargetDestination = Vector3.Distance(agent.destination, (Vector3)target);

            if (distanceTargetDestination > 0.2f)
            {
                Debug.Log("Human going to " + target);
                Debug.Log(distanceTargetDestination);
                agent.SetDestination((Vector3)target);
            }
            else
            {
                if (agent.remainingDistance <= agent.stoppingDistance + 1f || catStatus.getHiding())
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
            if (target != null)
                Gizmos.DrawLine(transform.position, (Vector3)target);
        }

        IEnumerator WaitAndGoHome()
        {
            yield return new WaitForSeconds(5);
            agent.SetDestination(Home.transform.position);
        }
    }
}