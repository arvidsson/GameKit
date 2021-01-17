using UnityEngine;

namespace UnityLib.Rendering
{
    /// <summary>
    /// All sprites with this behaviour will be sorted by the sprite sorter, so that sprites with lower y-coord will be displayed behind other sprites with higher y-coord.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteSortBehaviour : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            SpriteSorter.RegisterSprite(spriteRenderer);
        }

        private void OnDisable()
        {
            SpriteSorter.UnregisterSprite(spriteRenderer);
        }
    }
}