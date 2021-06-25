using UnityEditor;
using UnityEngine;

namespace Ftech.Lib.Common.UnityInspector.Editor.Editor
{
    [CustomPropertyDrawer(typeof(SpriteFieldAttribute))]
    public class SpriteFieldPropertyDrawner : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SpriteFieldAttribute spriteFieldAttribute = (SpriteFieldAttribute)attribute;
            return spriteFieldAttribute.size;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            EditorGUI.BeginChangeCheck();

            Object spriteField = property.objectReferenceValue;

            spriteField = EditorGUI.ObjectField(position, label, spriteField, typeof(Sprite), false);

            if (EditorGUI.EndChangeCheck())
            {
                property.objectReferenceValue = spriteField;
            }
        }
    }
}
