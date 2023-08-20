using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosCats
{
    public class TestNoise : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActionAsset;
        [SerializeField] private EventBus eventBus;

        private InputActionMap inputActionMap;

        private void Awake()
        {
            inputActionMap = inputActionAsset.FindActionMap("TestActions", true);
            inputActionMap.FindAction("MakeNoise", true).performed += (_) => {
                Debug.Log("MakeNoise Event fired");
                eventBus.MakeNoise?.Invoke();
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
