using UnityEngine;

namespace GameKit.Audio
{

    [CreateAssetMenu(menuName = "GameKit/Audio/Sound", fileName = "SingleSound")]
    public class SingleSound : Sound
    {
        public AudioClip clip;

        public override AudioClip GetClip() => clip;
    }

}