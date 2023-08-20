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

        void Start()
        {
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
                if (agent.remainingDistance < 1.5f || catStatus.getHiding())
                {
                    target = null;
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
            Vector3 originalVelocity = agent.velocity;
            agent.velocity = Vector3.zero;
            yield return new WaitForSeconds(5);
            agent.SetDestination(Home.transform.position);
            agent.velocity = originalVelocity;
        }
    }
}