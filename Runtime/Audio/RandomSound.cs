using UnityEngine;

namespace GameKit.Audio
{

    [CreateAssetMenu(menuName = "GameKit/Audio/RandomSound", fileName = "RandomSound")]
    public class RandomSound : Sound
    {
        public AudioClip[] clips;

        public override AudioClip GetClip() => clips[Random.Range(0, clips.Length)];
    }

}