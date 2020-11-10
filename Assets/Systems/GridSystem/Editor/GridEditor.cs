using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace GridSystem
{
    [CustomEditor(typeof(Grid))]
    public class GridEditor : Editor
    {
        private SerializedProperty editorTileData;

        private ReorderableList list;
        
        public void OnEnable()
        {
            editorTileData = serializedObject.FindProperty("editorTileData");
            
            list = new ReorderableList(serializedObject, editorTileData, false, false, false, false);
            list.drawHeaderCallback = (rect) =>
            {
                EditorGUI.LabelField(rect, "Grid Tiles - Count " + editorTileData.arraySize);
            };
            list.drawElementCallback = (rect, index, active, focused) =>
            {
                EditorGUI.PropertyField(rect, editorTileData.GetArrayElementAtIndex(index));
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            list.DoLayoutList();
        }
    }
}