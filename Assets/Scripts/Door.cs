using UnityEngine;

namespace ChaosCats
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private GameObject openedDoor;
        [SerializeField] private GameObject closedDoor;
        [SerializeField] private bool opened = false;
        private bool previouslyOpened;

        private void Update()
        {
            if (previouslyOpened != opened)
            {
                Toggle();
            }
        }

        private void Toggle()
        {
            //
        }
    }
}
