using UnityEngine;

namespace ChaosCats.Audio.Helpers
{
    [System.Serializable]
    public class CleanableAudioSource
    {
        public AudioSource audioSource;
        public float lastStoppedTime;
    }
}
