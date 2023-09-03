using System;
using UnityEngine;
using UnityEngine.Events;

namespace ChaosCats
{
    [CreateAssetMenu(fileName = "EventBus", menuName = "ScriptableObjects/EventBus")]
    public class EventBus : ScriptableObject
    {
        public UnityEvent MakeNoise;
        public UnityEvent PlayerCaught;
        public UnityEvent<string> LoadSceneWithoutDelay;
        public UnityEvent<string, float> LoadSceneWithDelay;
    }
}
