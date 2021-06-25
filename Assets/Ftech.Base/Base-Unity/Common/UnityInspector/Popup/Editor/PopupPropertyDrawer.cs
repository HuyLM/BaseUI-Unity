using UnityEditor;
using UnityEngine;

namespace Ftech.Lib.Common.UnityInspector.Editor.Editor {
    [CustomPropertyDrawer(typeof(PopupAttribute))]
    public class PopupPropertyDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            bool isInteger = property.propertyType == SerializedPropertyType.Integer;
            bool isString = property.propertyType == SerializedPropertyType.String;

            if (isInteger || isString) {
                PopupAttribute popupAttribute = attribute as PopupAttribute;

                EditorGUI.BeginChangeCheck();

                int selectedIndex = 0;

                if (isString) {
                    for (int i = 0; i < popupAttribute.options.Length; i++) {
                        if (popupAttribute.options[i] == property.stringValue) {
                            selectedIndex = i;
                            break;
                        }
                    }
                } else if (isInteger) {
                    selectedIndex = property.intValue;
                }

                selectedIndex = EditorGUI.Popup(position, selectedIndex, popupAttribute.options);

                if (EditorGUI.EndChangeCheck()) {
                    if (isString) {
                        property.stringValue = popupAttribute.options[selectedIndex];
                    } else if (isInteger) {
                        property.intValue = selectedIndex;
                    }
                }
            } else {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}