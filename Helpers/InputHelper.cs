using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityLib
{
    /// <summary>
    /// Helpers class for useful input functionality.
    /// </summary>
    public static class InputHelper
    {
        /// <summary>
        /// Returns true if the mouse is over a GUI object.
        /// </summary>
        public static bool IsMouseOverGUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

        /// <summary>
        /// Shows the hardware mouse cursor.
        /// </summary>
        public static void ShowMouse()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        /// <summary>
        /// Hides the hardware mouse cursor.
        /// </summary>
        public static void HideMouse()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}