using UnityEngine;

namespace GameKit.Events
{
    public class EventBehaviour : MonoBehaviour
    {
        public EventManager EventManager { get; private set; } = new EventManager();
    }
}