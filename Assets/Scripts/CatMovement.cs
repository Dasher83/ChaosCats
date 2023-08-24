using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace ChaosCats
{
    public class CatMovement : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActionAsset;
        [SerializeField] private float speed = 80f;
        [SerializeField] private float jumpForce = 8f;
        [SerializeField] private float distanceToObjects = 1f;
        [SerializeField] private CatStatus catStatus;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private float gravityScale = 1f;

        private Vector3 moveDirection;
        private Rigidbody rb;
        private InputActionMap inputActionMap;
        private Animator catAnimator;

        private bool isGrounded = true;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            inputActionMap = inputActionAsset.FindActionMap("PlayerActions", true);
            inputActionMap.FindAction("Move", true).performed += OnMovePerformed;
            inputActionMap.FindAction("Move", true).canceled += OnMoveCanceled;
            inputActionMap.FindAction("Jump", true).performed += OnJumpPerformed;
        }

        private void Start()
        {
            if (ServiceLocator.Instance.BackgroundMusicPlayer.IsPlaying) return;
            ServiceLocator.Instance.BackgroundMusicPlayer.Play("Main Theme");
            catAnimator = GetComponentInChildren<Animator>();
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            moveDirection = context.ReadValue<Vector3>();
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            moveDirection = Vector3.zero;
        }

        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            if (!isGrounded) return;
            catAnimator.SetTrigger("Jump");
            // make the jump
            rb.AddRelativeForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }

        private bool CanMove(Vector3 direction)
        {
            RaycastHit hit;
            // If there's an obstacle in the given direction within the specified distance, return false
            return !(Physics.Raycast(transform.position, direction, out hit, distanceToObjects));
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.catIsHidden) return;

            Vector3 camForward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;

            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 normalizedMoveDirection = moveDirection.normalized;

            Vector3 move = (camRight * normalizedMoveDirection.x + camForward * normalizedMoveDirection.y) * speed * Time.deltaTime;

            // if (isJumping) move = move * 0.75f;

            if (CanMove(move))
                rb.position += move;

            // rotate to face the direction of movement
            if (move != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), 0.15F);
                catAnimator.SetBool("IsWalking", true);
            }
            else
            {
                catAnimator.SetBool("IsWalking", false);
            }

            // Custom gravity
            rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collision");
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            if (collision.collider != null && collision.collider.tag == "Floor")
            {
                isGrounded = true;
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            Debug.Log("Collision Exit");
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            if (collision.collider != null && collision.collider.tag == "Floor")
            {
                isGrounded = false;
            }
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
