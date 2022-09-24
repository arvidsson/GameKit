using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameKit.Audio
{
    public class PlaySound : MonoBehaviour
    {
        public AudioClip clip;

        public void Play()
        {
            SoundManager.PlaySound(clip);
        }
    }
}