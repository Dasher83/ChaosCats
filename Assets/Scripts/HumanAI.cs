using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace ChaosCats
{
    public class HumanAI : MonoBehaviour
    {
        [SerializeField]
        private Transform Home;

        [SerializeField]
        private int closeObjectsToSearch = 2;

        private Vector3? _target = null;
        public Vector3? target
        {
            get { return _target; }
            set
            {
                if (value != null)
                {
                    // Normalize Y to 0 before setting target
                    Vector3 newValue = (Vector3)value;
                    newValue.y = 0;
                    NavMeshHit myNavHit;
                    if (NavMesh.SamplePosition(newValue, out myNavHit, 2, -1))
                    {
                        _target = myNavHit.position;
                    }
                    else
                    {
                        _target = newValue;
                    }
                }
                else
                {
                    _target = null;
                }
            }
        }

        private NavMeshAgent agent;
        private Animator animator;
        private GameObject[] interactableObjects;
        private int objectsSearched;

        void Start()
        {
            interactableObjects = GameObject.FindGameObjectsWithTag("Interactable");
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();
            objectsSearched = 0;
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
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    // Debug.Log("Reached destination");
                    target = null;
                    // Get the closest interactable object
                    Array.Sort(interactableObjects, (x, y) =>
                        Vector3.Distance(x.transform.position, transform.position).CompareTo(
                            Vector3.Distance(y.transform.position, transform.position)
                        )
                    );
                    if (objectsSearched < closeObjectsToSearch)
                    {
                        objectsSearched++;
                        StartCoroutine(WaitAndGoTo(interactableObjects[objectsSearched].transform.position));
                    }
                    else
                    {
                        objectsSearched = 0;
                        StartCoroutine(WaitAndGoHome());
                    }
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

        public void StartChasingPlayer()
        {
            Debug.Log("StartChasingPlayer");
            StopAllCoroutines();
        }

        IEnumerator WaitAndGoTo(Vector3 newDestination)
        {
            yield return new WaitForSeconds(5);
            target = newDestination;
        }

        IEnumerator WaitAndGoHome()
        {
            yield return new WaitForSeconds(5);
            agent.SetDestination(Home.position);
        }
    }
}