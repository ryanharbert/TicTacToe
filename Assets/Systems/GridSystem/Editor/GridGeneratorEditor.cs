using UnityEditor;
using UnityEngine;

namespace GridSystem
{
    [CustomEditor(typeof(GridGenerator))]
    public class GridGeneratorEditor : Editor
    {
        private GridGenerator generator;
        private Grid grid;
        
        public void OnEnable()
        {
            generator = (GridGenerator) target;
            grid = generator.GetComponent<Grid>();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (GUILayout.Button("Generate Grid", GUILayout.Height(25)))
            {
                GenerateGrid();
            }
        }

        void GenerateGrid()
        {
            Undo.RecordObject(grid, "Before Generate");
            
            generator.GenerateGrid();
            
            EditorUtility.SetDirty(grid);
            
            Debug.Log("Generated Grid!");
        }
    }
}