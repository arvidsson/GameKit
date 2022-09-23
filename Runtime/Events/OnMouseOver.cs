using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameKit.Events
{
    public class OnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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