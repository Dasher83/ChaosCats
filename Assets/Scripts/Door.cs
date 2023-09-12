using ChaosCats.Scriptables;
using UnityEngine;

namespace ChaosCats
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private GameObject openedDoor;
        [SerializeField] private GameObject closedDoor;
        [SerializeField] private GameSession gameSession;
        private bool opened;

        private void Start()
        {
            opened = false;
            SetDoorState();
            gameSession.MadeNoise.AddListener(Toggle);
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
            gameSession.MadeNoise.RemoveListener(Toggle);
        }
    }
}
