using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaosCats
{
    public class HumanAI : MonoBehaviour
    {
        public Vector3 target;

        void Update()
        {
            target = GameObject.Find("Player").transform.position;
        
        }

        void FixedUpdate()
        {
            if (target != null)
                GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(target);
        }

        void OnDrawGizmos() {
            if (target != null)
                Gizmos.DrawLine(transform.position, target);
        }
    }
}
