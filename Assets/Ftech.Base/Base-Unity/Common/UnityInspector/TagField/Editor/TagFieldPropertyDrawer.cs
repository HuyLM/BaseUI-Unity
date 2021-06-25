using UnityEngine;
using UnityEditor;

namespace Ftech.Lib.Common.UnityInspector.Editor.Editor {
    [CustomPropertyDrawer(typeof(TagFieldAttribute))]
    public class TagFieldPropertyDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            bool isString = property.propertyType == SerializedPropertyType.String;
            if (isString) {
                EditorGUI.BeginChangeCheck();

                string tagField = property.stringValue;

                tagField = EditorGUI.TagField(position, label, tagField);

                if (EditorGUI.EndChangeCheck()) {
                    property.stringValue = tagField;
                }
            } else {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}