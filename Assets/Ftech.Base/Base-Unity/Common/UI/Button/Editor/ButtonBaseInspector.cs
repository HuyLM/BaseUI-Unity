using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace Ftech.Lib.UI
{
    [CustomEditor(typeof(ButtonBase))]
    public class ButtonBaseInspector : ButtonEditor
    {
        private ButtonBase button1;
        private SerializedProperty clickScaleProperty;
        private SerializedProperty invokeOnceProperty;
        protected override void OnEnable()
        {
            base.OnEnable();
            button1 = target as ButtonBase;
            clickScaleProperty = serializedObject.FindProperty("clickScale");
            invokeOnceProperty = serializedObject.FindProperty("invokeOnce");

        }
        public override void OnInspectorGUI()
        {
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour((ButtonBase)target), typeof(ButtonBase), false);
            GUI.enabled = true;
            GUILayout.Space(20);
            base.OnInspectorGUI();
            EditorGUILayout.PropertyField(clickScaleProperty);
            EditorGUILayout.PropertyField(invokeOnceProperty);

        }
    }
}