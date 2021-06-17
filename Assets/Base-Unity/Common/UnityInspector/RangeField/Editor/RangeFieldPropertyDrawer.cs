using UnityEngine;
using UnityEditor;

namespace GameSystem.Common.UnityInspector.Editor {
    [CustomPropertyDrawer(typeof(RangeFieldAttribute))]
    public class RangeFieldPropertyDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            RangeFieldAttribute rangeAttribute = (RangeFieldAttribute)attribute;

            bool isFloat = property.propertyType == SerializedPropertyType.Float;
            bool isInteger = property.propertyType == SerializedPropertyType.Integer;
            if (isFloat || isInteger) {
                // Draw the slider
                EditorGUI.BeginChangeCheck();

                float value;
                if (isFloat) {
                    value = property.floatValue;
                } else {
                    value = property.intValue;
                }

                value = EditorGUI.Slider(position, label, value, rangeAttribute.minValue, rangeAttribute.maxValue);

                if (EditorGUI.EndChangeCheck()) {
                    if (rangeAttribute.snapValue > 0f) {
                        value = Mathf.Round(value / rangeAttribute.snapValue) * rangeAttribute.snapValue;
                    }
                    if (isInteger) {
                        property.intValue = (int)value;
                    } else {
                        property.floatValue = value;
                    }
                }
            } else {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}
