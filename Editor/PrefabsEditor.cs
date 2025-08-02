using UnityEditor;
using UnityEngine;

namespace GameKit
{

    [CustomEditor(typeof(Prefabs))]
    public class PrefabsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Prefabs prefabManager = (Prefabs)target;

            GUILayout.Space(10);

            if (GUILayout.Button("Load Prefabs"))
            {
                prefabManager.LoadPrefabs();
            }

            if (GUILayout.Button("Clear Prefabs"))
            {
                prefabManager.ClearPrefabs();
            }
        }
    }

}