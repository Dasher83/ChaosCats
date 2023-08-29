using UnityEngine;

namespace ChaosCats
{
    public class CatchCat : MonoBehaviour
    {
        [SerializeField] private float catchingDistance;
        private Transform player;
        private EventBus eventBus;

        private bool isInCatchingRange => (player.transform.position - transform.position).magnitude < catchingDistance;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag(tag: "Player").transform;
            eventBus = Resources.Load<EventBus>("EventBus");
        }

        private void Update()
        {
            if (GameManager.Instance.runOver || GameManager.Instance.catIsHidden || isInCatchingRange == false) return;

            eventBus.PlayerCaught?.Invoke();
        }
    }
}
