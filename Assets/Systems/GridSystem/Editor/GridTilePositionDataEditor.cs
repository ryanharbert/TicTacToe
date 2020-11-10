using UnityEditor;
using UnityEngine;

namespace GridSystem
{
    [CustomPropertyDrawer(typeof(GridTilePositionData))]
    public class GridTilePositionDataEditor : PropertyDrawer
    {
        private GridTilePositionData namer;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            var posRect = new Rect(position.x, position.y, position.width / 2, EditorGUIUtility.singleLineHeight);
            var tileRect = new Rect(position.x + position.width / 2, position.y, position.width / 2, EditorGUIUtility.singleLineHeight);

            EditorGUI.BeginDisabledGroup(true);
            EditorGUIUtility.labelWidth = 65;
            EditorGUI.PropertyField(posRect, property.FindPropertyRelative(nameof(namer.Position)));
            EditorGUIUtility.labelWidth = 35;
            EditorGUI.PropertyField(tileRect, property.FindPropertyRelative(nameof(namer.Tile)));
            EditorGUI.EndDisabledGroup();
            
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}