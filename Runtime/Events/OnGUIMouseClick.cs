using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameKit.Events
{
    /// <summary>
    /// Detects mouse clicks over GUI and fires the event.
    /// </summary>
    public class OnGUIMouseClick : MonoBehaviour, IPointerClickHandler
    {
        public UnityEvent onMouseClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            onMouseClick?.Invoke();
        }
    }
}