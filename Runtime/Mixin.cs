using UnityEngine;

namespace GameKit
{
    /// <summary>
    /// A Mixin is an object that adds behaviour to a Mix object (eg fire damage to a sword).
    /// </summary>
    public abstract class Mixin<T> : MonoBehaviour
    {
    }

    /// <summary>
    /// A Mix is an object which has Mixin components which add flavour to the Mix object. Eg, a sword mix can have different mixins to add fire damage, ice damage etc.
    /// </summary>
    public abstract class Mix : MonoBehaviour
    {
        public T[] GetMixins<T>() where T : Mixin<T>
        {
            return GetComponentsInChildren<T>();
        }
    }
}