using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameKit.State
{
    public class ControlStack : MonoBehaviour
    {
        public static List<ControlElement> Controls = new List<ControlElement>();
        private ControlElement current;

        private void Update()
        {
            if (Controls.Count == 0) return;

            if (current != Controls.Last())
            {
                if (current != null)
                {
                    current.OnExit();
                }

                current = Controls.Last();

                current.OnEnter();
            }

            current.OnUpdate();
        }
    }
}