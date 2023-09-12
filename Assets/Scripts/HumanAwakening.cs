using ChaosCats.Scriptables;
using UnityEngine;

namespace ChaosCats
{
    public class HumanAwakening : MonoBehaviour
    {
        [SerializeField] private GameObject human;
        [SerializeField] private GameObject spawnPoint;
        [SerializeField] private GameSession gameSession;

        private void Start()
        {
            gameSession.MadeNoise.AddListener(WakeUpHuman);
            human.SetActive(false);
        }

        public void OnDisable()
        {
            gameSession.MadeNoise?.RemoveListener(WakeUpHuman);
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
