using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace ChaosCats
{
    public class CatMovement : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActionAsset;
        [SerializeField] private float speed = 80f;
        [SerializeField] private CatStatus catStatus;
        [SerializeField] private float obstacleDistance = 0.1f;
        [SerializeField] private NavMeshAgent navMeshAgent;

        private Vector3 moveDirection;
        private Rigidbody rb;
        private InputActionMap inputActionMap;

        private void Start()
        {
            catStatus.initHiding();
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            inputActionMap = inputActionAsset.FindActionMap("PlayerActions", true);
            inputActionMap.FindAction("Move", true).performed += OnMovePerformed;
            inputActionMap.FindAction("Move", true).canceled += OnMoveCanceled;
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            moveDirection = context.ReadValue<Vector3>();
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            moveDirection = Vector3.zero;
        }

        private bool CanMove(Vector3 direction)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, obstacleDistance))
            {
                // If there's an obstacle in the given direction within the specified distance, return false
                return false;
            }
            return true;
        }

        private void FixedUpdate()
        {
            if (catStatus.getHiding()) return;

            Vector3 camForward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;

            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 move = (camRight * moveDirection.x + camForward * moveDirection.y) * speed * Time.deltaTime;

            /*if (CanMove(move.normalized))
            {
                rb.MovePosition(transform.position + move);
            }*/

            navMeshAgent.Move(move);
        }

        private void OnEnable()
        {
            inputActionMap.Enable();
        }

        private void OnDisable()
        {
            inputActionMap.Disable();
        }
    }
}
