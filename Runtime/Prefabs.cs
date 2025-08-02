using System.Collections.Generic;
using UnityEngine;

namespace GameKit
{

    public class Prefabs : Singleton<Prefabs>
    {
        [SerializeField] string prefabsFolder = "Assets/Your/Prefab/Folder";
        [SerializeField] List<GameObject> prefabs = new();

        public void LoadPrefabs()
        {
#if UNITY_EDITOR
            prefabs.Clear();

            string[] guids = UnityEditor.AssetDatabase.FindAssets("t:GameObject", new[] { "Assets/" + prefabsFolder });

            foreach (string guid in guids)
            {
                string assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
                GameObject prefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                if (prefab != null)
                    prefabs.Add(prefab);
            }

            UnityEditor.EditorUtility.SetDirty(this);
            Debug.Log($"Loaded {prefabs.Count} prefabs from {prefabsFolder}");
#endif
        }

        public void ClearPrefabs()
        {
#if UNITY_EDITOR
            prefabs.Clear();
            UnityEditor.EditorUtility.SetDirty(this);
            Debug.Log("Cleared prefab list");
#endif
        }

        public GameObject GetPrefabByName(string name)
        {
            return prefabs.Find(p => p != null && p.name == name);
        }
    }
}