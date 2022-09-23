using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameKit.State
{
    public class ControlElement : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            ControlStack.Controls.Add(this);
        }

        protected virtual void OnDisable()
        {
            ControlStack.Controls.Remove(this);
        }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void OnUpdate() { }
    }
}