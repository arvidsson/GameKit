using UnityEngine;
using UnityEngine.Events;

namespace GameKit.Events
{
    /// <summary>
    /// Detects mouse clicks over colliders and fires the event.
    /// </summary>
    public class OnMouseButton : MonoBehaviour
    {
        public UnityEvent onMouseClick;

        private void OnMouseDown()
        {
            onMouseClick?.Invoke();
        }
    }
}