using UnityEngine;

namespace ChaosCats
{
    public class CatchCat : MonoBehaviour
    {
        [SerializeField] private EventBus eventBus;
        [SerializeField] private Transform player;
        [SerializeField] private float inRangeDistance;
        private bool playerInRange;

        private void Start()
        {
            playerInRange = false; 
        }

        private void Update()
        {
            if (GameManager.Instance.runOver) return;
            CheckPlayerInRange();
            if (playerInRange && !GameManager.Instance.catIsHidden) eventBus.PlayerCaught?.Invoke();
        }

        private void CheckPlayerInRange()
        {
            Vector3 distanceToPlayer = (player.transform.position - transform.position);
            playerInRange = distanceToPlayer.magnitude < inRangeDistance;
        }
    }
}
