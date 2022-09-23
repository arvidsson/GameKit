using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameKit.Events
{
    public class OnStart : MonoBehaviour
    {
        public UnityEvent onStart;

        private void Start()
        {
            onStart?.Invoke();
        }
    }
}