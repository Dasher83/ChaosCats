using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ChaosCats
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private GameObject openedDoor;
        [SerializeField] private GameObject closedDoor;
        [SerializeField] private EventBus eventBus;
        private bool opened;

        private void Start()
        {
            opened = false;
            SetDoorState();
            eventBus.ToggleDoor.AddListener(Toggle);
        }

        private void Toggle()
        {
            Debug.Log("Door toggled!");
            opened = !opened;
            SetDoorState();
        }

        private void SetDoorState()
        {
            openedDoor.SetActive(opened);
            closedDoor.SetActive(opened == false);
        }

        private void OnDisable()
        {
            eventBus.ToggleDoor.RemoveListener(Toggle);
        }
    }
}
