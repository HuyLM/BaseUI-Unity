using UnityEngine;
using UnityEditor;

namespace Ftech.Lib.Common.UnityInspector.Editor.Editor {
    [CustomPropertyDrawer(typeof(MinMaxSliderAttribute))]
    public class MinMaxSliderPropertyDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            MinMaxSliderAttribute minMaxSliderAttribute = (MinMaxSliderAttribute)attribute;

            bool isVector2 = property.propertyType == SerializedPropertyType.Vector2;
            bool isVector2Int = property.propertyType == SerializedPropertyType.Vector2Int;
            if (isVector2 || isVector2Int) {
                // Draw the label
                Rect controlRect = EditorGUI.PrefixLabel(position, label);
                float sliderPadding = 5f;
                float fieldWidth = EditorGUIUtility.fieldWidth;
                float sliderWidth = controlRect.width - 2f * (fieldWidth + sliderPadding);

                Rect minFieldRect = new Rect(
                    controlRect.x,
                    controlRect.y,
                    fieldWidth,
                    controlRect.height);

                Rect sliderRect = new Rect(
                    controlRect.x + fieldWidth + sliderPadding,
                    controlRect.y,
                    sliderWidth,
                    controlRect.height);

                Rect maxFieldRect = new Rect(
                    controlRect.x + fieldWidth + sliderWidth + 2f * sliderPadding,
                    controlRect.y,
                    fieldWidth,
                    controlRect.height);
                
                // Draw the slider
                EditorGUI.BeginChangeCheck();
                int indent = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;

                Vector2 sliderValue;
                if (isVector2) {
                    sliderValue = property.vector2Value;
                } else {
                    sliderValue = property.vector2IntValue;
                }
                EditorGUI.MinMaxSlider(sliderRect, ref sliderValue.x, ref sliderValue.y, minMaxSliderAttribute.minValue, minMaxSliderAttribute.maxValue);

                sliderValue.x = EditorGUI.FloatField(minFieldRect, sliderValue.x);
                sliderValue.x = Mathf.Clamp(sliderValue.x, minMaxSliderAttribute.minValue, Mathf.Min(minMaxSliderAttribute.maxValue, sliderValue.y));

                sliderValue.y = EditorGUI.FloatField(maxFieldRect, sliderValue.y);
                sliderValue.y = Mathf.Clamp(sliderValue.y, Mathf.Max(minMaxSliderAttribute.minValue, sliderValue.x), minMaxSliderAttribute.maxValue);

                EditorGUI.indentLevel = indent;
                if (EditorGUI.EndChangeCheck()) {
                    if (minMaxSliderAttribute.snapValue > 0f) {
                        sliderValue.x = Mathf.Round(sliderValue.x / minMaxSliderAttribute.snapValue) * minMaxSliderAttribute.snapValue;
                        sliderValue.y = Mathf.Round(sliderValue.y / minMaxSliderAttribute.snapValue) * minMaxSliderAttribute.snapValue;
                    }
                    if (isVector2Int) {
                        property.vector2IntValue = new Vector2Int((int)sliderValue.x, (int)sliderValue.y);
                    } else {
                        property.vector2Value = sliderValue;
                    }
                }
            } else {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}