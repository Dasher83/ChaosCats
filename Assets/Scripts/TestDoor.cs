using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosCats
{
    public class TestDoor : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActionAsset;
        [SerializeField] private EventBus eventBus;

        private InputActionMap inputActionMap;

        private void Awake()
        {
            inputActionMap = inputActionAsset.FindActionMap("TestActions", true);
            inputActionMap.FindAction("ToggleDoor", true).performed += (_) => {
                Debug.Log("Door Event fired");
                eventBus.ToggleDoor?.Invoke();
            };
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
