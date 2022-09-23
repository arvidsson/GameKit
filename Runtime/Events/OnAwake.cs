using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameKit.Events
{
    public class OnAwake : MonoBehaviour
    {
        public UnityEvent onAwake;

        private void Awake()
        {
            onAwake?.Invoke();
        }
    }
}