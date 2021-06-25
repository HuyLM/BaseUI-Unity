using UnityEditor;
using UnityEngine;

namespace Ftech.Lib.Common.UnityInspector.Editor.Editor {
    [CustomPropertyDrawer(typeof(LayerFieldAttribute))]
    public class LayerFieldPropertyDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            bool isInteger = property.propertyType == SerializedPropertyType.Integer;

            if (isInteger) {
                EditorGUI.BeginChangeCheck();

                int layerField = property.intValue;

                layerField = EditorGUI.LayerField(position, label, layerField);

                if (EditorGUI.EndChangeCheck()) {
                    property.intValue = layerField;
                }
            } else {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}