using UnityEngine;

namespace ChaosCats
{
    public class HumanAwakening : MonoBehaviour
    {
        [SerializeField] private GameObject human;
        [SerializeField] private GameObject spawnPoint;
        [SerializeField] private EventBus eventBus;

        private void Start()
        {
            eventBus.MakeNoise.AddListener(WakeUpHuman);
            human.SetActive(false);
        }

        private void WakeUpHuman()
        {
            if (human.activeInHierarchy) return;

            human.SetActive(true);
            Debug.Log("Human activated");
            GameManager.Instance.UpdateHumanStateUI(true);
            ServiceLocator.Instance.SoundEffectPlayer.Play("wakeUpSound");
            human.transform.position = spawnPoint.transform.position;
        }
    }
}
