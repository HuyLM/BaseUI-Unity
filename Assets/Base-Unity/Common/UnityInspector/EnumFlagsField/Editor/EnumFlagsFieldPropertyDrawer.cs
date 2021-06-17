using System;
using UnityEditor;
using UnityEngine;

namespace GameSystem.Common.UnityInspector.Editor {
    [CustomPropertyDrawer(typeof(EnumFlagsFieldAttribute))]
    public class EnumFlagsFieldPropertyDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            bool isEnum = property.propertyType == SerializedPropertyType.Enum;

            if (isEnum) {
                Type type = fieldInfo.FieldType;

                Enum enumValue = null;
                if (type.IsArray) {
                    enumValue = (Enum)Enum.ToObject(type.GetElementType(), property.intValue);
                } else {
                    enumValue = (Enum)Enum.ToObject(type, property.intValue);
                }

                EditorGUI.BeginChangeCheck();

                Enum enumField = EditorGUI.EnumFlagsField(position, label, enumValue);

                if (EditorGUI.EndChangeCheck()) {
                    property.intValue = (int)Convert.ChangeType(enumField, enumValue.GetType());
                }
            } else {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}