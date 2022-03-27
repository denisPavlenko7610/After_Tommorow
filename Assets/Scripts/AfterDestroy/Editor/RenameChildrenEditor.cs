using UnityEditor;
using UnityEngine;

namespace AfterDestroy.Editor
{
    public class RenameChildrenEditor : EditorWindow
    {
        private static readonly Vector2Int size = new Vector2Int(250, 100);
        private string childrenPrefix;
        private int startIndex;

        [MenuItem("GameObject/Rename children")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow<RenameChildrenEditor>();
            window.minSize = size;
            window.maxSize = size;
        }

        private void OnGUI()
        {
            childrenPrefix = EditorGUILayout.TextField("Children prefix", childrenPrefix);
            startIndex = EditorGUILayout.IntField("Start index", startIndex);
            if (GUILayout.Button("Rename children"))
            {
                GameObject[] selectedObjects = Selection.gameObjects;
                for (int objectIndex = 0; objectIndex < selectedObjects.Length; objectIndex++)
                {
                    Transform selectedObjectT = selectedObjects[objectIndex].transform;
                    for (int childIndex = 0, i = startIndex; childIndex < selectedObjectT.childCount; childIndex++)
                        selectedObjectT.GetChild(childIndex).name = $"{childrenPrefix}{i++}";
                }
            }
        }
    }
}