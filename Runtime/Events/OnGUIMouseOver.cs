using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameKit.Events
{
    /// <summary>
    /// Detects mouse enter/exit over GUI and fires the respective event.
    /// </summary>
    public class OnGUIMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public UnityEvent onMouseEnter;
        public UnityEvent onMouseExit;

        public void OnPointerEnter(PointerEventData eventData)
        {
            onMouseEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onMouseExit?.Invoke();
        }
    }
}