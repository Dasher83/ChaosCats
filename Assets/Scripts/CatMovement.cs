using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosCats
{
    public class CatMovement : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActionAsset;

        public float speed = 5f;
        private Vector3 moveDirection;
        private Rigidbody rb;
        private InputActionMap inputActionMap;

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

        private void Update()
        {
            Vector3 move = new Vector3(moveDirection.x, 0, moveDirection.y) * speed * Time.deltaTime;
            transform.Translate(move);
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
