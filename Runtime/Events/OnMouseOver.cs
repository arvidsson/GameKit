using UnityEngine;
using UnityEngine.Events;

namespace GameKit.Events
{
    /// <summary>
    /// Detects mouse enter/exit over colliders and fires the respective event.
    /// </summary>
    public class OnMouseOver : MonoBehaviour
    {
        public UnityEvent onMouseEnter;
        public UnityEvent onMouseExit;

        private void OnMouseEnter()
        {
            onMouseEnter?.Invoke();
        }

        private void OnMouseExit()
        {
            onMouseExit?.Invoke();
        }
    }
}