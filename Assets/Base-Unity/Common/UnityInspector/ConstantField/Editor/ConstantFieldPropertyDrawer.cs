using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Ftech.Lib.Common.UnityInspector.Editor {
    [CustomPropertyDrawer(typeof(ConstantFieldAttribute))]
    public class ConstantFieldpropertyDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

            ConstantFieldAttribute constantField = attribute as ConstantFieldAttribute;

            IEnumerable<FieldInfo> fieldInfos = constantField.type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                                                                  .Where(fi => fi.IsLiteral && !fi.IsInitOnly);

            List<FieldInfo> fieldInfosToShow = new List<FieldInfo>();
            Type type = GetPropertyType(property);

            foreach (FieldInfo fieldInfo in fieldInfos) {
                if (fieldInfo.FieldType == type) {
                    fieldInfosToShow.Add(fieldInfo);
                }
            }
            int selectedIndex = 0;

            GUIContent[] gUIContents = new GUIContent[fieldInfosToShow.Count];

            for (int i = 0; i < fieldInfosToShow.Count; i++) {
                FieldInfo fieldInfo = fieldInfosToShow[i];
                gUIContents[i] = new GUIContent(fieldInfo.Name);
                if (ComparerValue(fieldInfo, property)) {
                    selectedIndex = i;
                }
            }

            EditorGUI.BeginChangeCheck();

            selectedIndex = EditorGUI.Popup(position, label, selectedIndex, gUIContents);

            if (EditorGUI.EndChangeCheck()) {
                SetValue(property, fieldInfosToShow[selectedIndex]);
            }
        }

        private Type GetPropertyType(SerializedProperty property) {
            switch (property.propertyType) {
                case SerializedPropertyType.Integer:
                    return typeof(int);
                case SerializedPropertyType.Boolean:
                    return typeof(bool);
                case SerializedPropertyType.Float:
                    return typeof(float);
                case SerializedPropertyType.String:
                    return typeof(string);
                default:
                    throw new Exception("Invalid type: " + property.propertyType.ToString());
            }
        }

        private bool ComparerValue(FieldInfo fieldInfo, SerializedProperty property) {
            switch (property.propertyType) {
                case SerializedPropertyType.Integer:
                    return fieldInfo.GetValue(null).Equals(property.intValue);
                case SerializedPropertyType.Boolean:
                    return fieldInfo.GetValue(null).Equals(property.boolValue);
                case SerializedPropertyType.Float:
                    return fieldInfo.GetValue(null).Equals(property.floatValue);
                case SerializedPropertyType.String:
                    return fieldInfo.GetValue(null).Equals(property.stringValue);
                default:
                    throw new Exception("Invalid type: " + property.propertyType.ToString());
            }
        }

        private void SetValue(SerializedProperty property, FieldInfo fieldInfo) {
            switch (property.propertyType) {
                case SerializedPropertyType.Integer:
                    property.intValue = System.Convert.ToInt32(fieldInfo.GetValue(null));
                    break;
                case SerializedPropertyType.Boolean:
                    property.boolValue = System.Convert.ToBoolean(fieldInfo.GetValue(null));
                    break;
                case SerializedPropertyType.Float:
                    property.floatValue = (float)System.Convert.ToDouble(fieldInfo.GetValue(null));
                    break;
                case SerializedPropertyType.String:
                    property.stringValue = System.Convert.ToString(fieldInfo.GetValue(null));
                    break;
                default:
                    throw new Exception("Invalid type: " + property.propertyType.ToString());
            }
        }
    }
}