using UnityEditor;
using UnityEngine;
using static Ftech.Lib.UI.ButtonExplorer;

namespace Ftech.Lib.UI
{
    [CustomEditor(typeof(ButtonExplorer))]
    public class ButtonExplorerInspector : ButtonBaseInspector
    {
        protected ButtonExplorer buttonExplorer;
        private SerializedProperty mainBgProperty;
        private SerializedProperty clickSoundEnableProperty;
        private SerializedProperty clickSoundEffectProperty;
        private SerializedProperty disableTypeProperty;
        private SerializedProperty disableMaskProperty;
        private SerializedProperty enableColorProperty;
        private SerializedProperty disableColorProperty;
        private SerializedProperty enableMatProperty;
        private SerializedProperty disableMatProperty;
        private SerializedProperty enableSpriteProperty;
        private SerializedProperty disableSpriteProperty;



        protected override void OnEnable()
        {
            base.OnEnable();
            buttonExplorer = target as ButtonExplorer;
            mainBgProperty = serializedObject.FindProperty("mainBg");

            clickSoundEnableProperty = serializedObject.FindProperty("clickSoundEnable");
            clickSoundEffectProperty = serializedObject.FindProperty("clickSoundEffect");

            disableTypeProperty = serializedObject.FindProperty("disableType");

            disableMaskProperty = serializedObject.FindProperty("disableMask");

            enableColorProperty = serializedObject.FindProperty("enableColor");
            disableColorProperty = serializedObject.FindProperty("disableColor");

            enableMatProperty = serializedObject.FindProperty("enableMat");
            disableMatProperty = serializedObject.FindProperty("disableMat");

            enableSpriteProperty = serializedObject.FindProperty("enableSprite");
            disableSpriteProperty = serializedObject.FindProperty("disableSprite");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.PropertyField(mainBgProperty);

            EditorGUILayout.PropertyField(clickSoundEnableProperty);
            if (clickSoundEnableProperty.boolValue)
            {
                EditorGUILayout.PropertyField(clickSoundEffectProperty);
            }
            EditorGUILayout.PropertyField(disableTypeProperty);
            DisableType type = buttonExplorer.MyDisableType;
            if (type == DisableType.NONE)
            {

            }
            else if (type == DisableType.MASK)
            {
                EditorGUILayout.PropertyField(disableMaskProperty);
            }
            else if (type == DisableType.COLOR)
            {
                EditorGUILayout.PropertyField(enableColorProperty);
                EditorGUILayout.PropertyField(disableColorProperty);
            }
            else if (type == DisableType.MATERIAL)
            {
                EditorGUILayout.PropertyField(enableMatProperty);
                EditorGUILayout.PropertyField(disableMatProperty);
            }
            else if (type == DisableType.SPRITE)
            {
                EditorGUILayout.PropertyField(enableSpriteProperty);
                EditorGUILayout.PropertyField(disableSpriteProperty);
            }

            if (GUI.changed)
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
