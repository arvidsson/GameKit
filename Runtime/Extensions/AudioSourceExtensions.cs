using System;
using System.Collections;
using UnityEngine;

namespace UnityEngine
{
    public static class AudioSourceExtensions
    {
        /// <summary>
        /// Fades out an audiosource by lowering the volume until it's 0.
        /// </summary>
        public static IEnumerator FadeOut(this AudioSource audioSource, float duration, Action onComplete = null)
        {
            var startingVolume = audioSource.volume;

            while (audioSource.volume > 0.0f)
            {
                audioSource.volume -= Time.deltaTime * startingVolume / duration;
                yield return null;
            }

            onComplete?.Invoke();
        }

        /// <summary>
        /// Fades in an audiosource by raising the volume until it reaches the target volume (1f by default).
        /// </summary>
        public static IEnumerator FadeIn(this AudioSource audioSource, float duration, float targetVolume = 1f, Action onComplete = null)
        {
            audioSource.volume = 0f;

            while (audioSource.volume < targetVolume)
            {
                audioSource.volume += Time.deltaTime * targetVolume / duration;
                yield return null;
            }

            onComplete?.Invoke();
        }
    }
}