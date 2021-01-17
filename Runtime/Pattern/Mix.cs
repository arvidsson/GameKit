using UnityEngine;

namespace UnityLib.Pattern
{
    /// <summary>
    /// A Mix is a gameobject which has mixin components which add flavour to the mix object. Eg, a sword mix can have different mixins to add fire damage, ice damage etc.
    /// </summary>
    public abstract class Mix : MonoBehaviour
    {
        public T[] GetMixins<T>() where T : Mixin<T>
        {
            return GetComponentsInChildren<T>();
        }
    }
}