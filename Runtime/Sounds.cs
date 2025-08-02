using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameKit
{
    // TODO: sound assets scriptable object (single sound, random sound, sequence, spatial etc), spatial sounds, looping sounds etc
    // so pooling of audio sources using Pool, getting a ref to a sound that is playing so we can stop it and thus return it to the pool
    public class Sounds : Singleton<Sounds>
    {
        [SerializeField] private int preloadCount = 5;

        private List<AudioSource> audioSources = new();

        public static void PlaySound(AudioClip clip, float volume = 1f, float pitch = 1f, bool spatialize = false)
        {
            var audioSource = Instance.GetAudioSourceFromPool();
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.spatialBlend = spatialize ? 1f : 0f;
            audioSource.Play();
        }

        protected override void OnSingletonAwake()
        {
            PreloadAudioSources(preloadCount);
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

        private void PreloadAudioSources(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var source = gameObject.AddComponent<AudioSource>();
                source.enabled = false;
                audioSources.Add(source);
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

            audioSource.enabled = true;
            return audioSource;
        }

        private void ReturnAudioSourceToPool(AudioSource audioSource)
        {
            audioSource.clip = null;
            audioSource.enabled = false;
        }
    }
}