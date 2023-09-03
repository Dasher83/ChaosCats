using ChaosCats.Scriptables;
using TMPro;
using UnityEngine;

namespace ChaosCats
{
    public class SetMainMenuScore : MonoBehaviour
    {
        private GameSession lastGameSession;

        private void Start()
        {
            lastGameSession = Resources.Load<GameSession>("GameSession");
            GetComponent<TextMeshProUGUI>().text = "Score: " + lastGameSession.PlayerScore;
        }
    }
}
