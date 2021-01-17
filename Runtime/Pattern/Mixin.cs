using UnityEngine;

namespace UnityLib.Pattern
{
    /// <summary>
    /// A Component is a MonoBehaviour that has easy access to a Main MonoBehaviour that can either be on the same GameObject, or in a parent GameObject.
    /// </summary>
    public abstract class Mixin<T> : MonoBehaviour
    {
    }
}