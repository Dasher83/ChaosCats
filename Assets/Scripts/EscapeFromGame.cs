using UnityEngine;
using UnityEngine.InputSystem;

namespace ChaosCats
{
    public class EscapeFromGame : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActionAsset;

        private InputActionMap inputActionMap;

        private void Start()
        {
            inputActionMap = inputActionAsset.FindActionMap("PlayerActions", true);
            inputActionMap.FindAction("QuitGame", true).performed += Escape;
        }

        public void Escape(InputAction.CallbackContext context)
        {
            Debug.Log("Escape rope");
            Application.Quit();
        }
    }
}
