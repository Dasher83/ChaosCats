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

        public Vector3? target = null;

        private NavMeshAgent agent;

        void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform.position;
            agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if (target == null) return;

            float distanceTargetDestination = Vector3.Distance(agent.destination, (Vector3)target);

            if (distanceTargetDestination > 0.2f)
            {
                Debug.Log("Human going to " + target);
                Debug.Log(distanceTargetDestination);
                agent.SetDestination((Vector3)target);
            }
            else
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    target = null;
                    StartCoroutine(WaitAndGoHome());
                }
            }
        }

        void OnDrawGizmos() {
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
