using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameKit.Audio
{
    public class SoundManager : Singleton<SoundManager>
    {
        private List<AudioSource> audioSources;

        public static void PlaySound(AudioClip clip)
        {
            var audioSource = Instance.GetAudioSourceFromPool();
            audioSource.clip = clip;
            audioSource.Play();
        }

        protected override void OnSingletonAwake()
        {
            audioSources = new List<AudioSource>();
        }

        protected override void OnSingletonDestroy()
        {
            audioSources.Clear();
        }

        private void Update()
        {
            foreach (var audioSource in audioSources.Where(x => x.enabled == true))
            {
                if (!audioSource.isPlaying)
                {
                    ReturnAudioSourceToPool(audioSource);
                }
            }
        }

        private AudioSource GetAudioSourceFromPool()
        {
            var audioSource = audioSources.Find(x => x.enabled == false);

            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSources.Add(audioSource);
            }

            return audioSource;
        }

        private void ReturnAudioSourceToPool(AudioSource audioSource)
        {
            audioSource.enabled = false;
        }
    }
}