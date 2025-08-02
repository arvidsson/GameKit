using UnityEngine;

namespace GameKit.Audio
{

    public abstract class Sound : ScriptableObject
    {
        public abstract AudioClip GetClip();
        [Range(0f, 1f)] public float volume = 1f;
        [Range(-3f, 3f)] public float pitch = 1f;
        public bool spatialize = false;
    }

}