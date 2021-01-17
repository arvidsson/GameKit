using UnityEngine;
using System.Collections.Generic;

namespace UnityLib.Rendering
{
    /// <summary>
    /// Simple animation using multiple sprites and showing them in a sequence.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class SimpleAnimationBehaviour : MonoBehaviour
    {
        public List<Sprite> frames;
        [Tooltip("Delay between switching frames")]
        public float animationDelay = 0.5f;

        private SpriteRenderer spriteRenderer;
        private int animationIndex = 0;
        private float animationTimer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animationTimer = Random.Range(0f, animationDelay);
        }

        private void Update()
        {
            if (Time.time > animationTimer)
            {
                animationIndex = ++animationIndex >= frames.Count ? 0 : animationIndex;
                spriteRenderer.sprite = frames[animationIndex];
                animationTimer = Time.time + animationDelay;
            }
        }
    }
}