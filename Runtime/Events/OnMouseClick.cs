using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameKit.Events
{
    public class OnMouseClick : MonoBehaviour, IPointerClickHandler
    {
        public UnityEvent onMouseClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            onMouseClick?.Invoke();
        }
    }
}