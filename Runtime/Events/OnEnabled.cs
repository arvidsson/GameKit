using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameKit.Events
{
    public class OnEnabled : MonoBehaviour
    {
        public UnityEvent onEnable;
        public UnityEvent onDisable;

        private void OnEnable()
        {
            onEnable?.Invoke();
        }

        private void OnDisable()
        {
            onDisable?.Invoke();
        }
    }
}