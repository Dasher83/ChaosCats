using UnityEngine;
using UnityEngine.Events;

namespace ChaosCats
{
    [CreateAssetMenu(fileName = "EventBus", menuName = "ScriptableObjects/EventBus")]
    public class EventBus : ScriptableObject
    {
        public UnityEvent ToggleDoor;
    }
}
