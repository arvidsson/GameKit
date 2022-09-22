using System.Linq;
using UnityEngine;

namespace GameKit.Pattern
{
    /// <summary>
    /// A prefabs object contains children who themselves are prefabs.
    /// This makes it easy to setup prefabs and get them during runtime.
    /// </summary>
    public class Prefabs : MonoBehaviour
    {
        public T GetPrefab<T>(string name) where T : MonoBehaviour
        {
            return GetComponentsInChildren<T>().SingleOrDefault(x => x.name == name);
        }
    }
}