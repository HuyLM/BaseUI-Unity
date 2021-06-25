using UnityEditor;
using UnityEngine;

namespace Ftech.Lib.UI
{
    [CustomEditor(typeof(DOTweenSizeDelta))]
    public class DOTweenSizeDeltaInspector : DOTweenTransitionInspector
    {
        private DOTweenSizeDelta dOTweenSizeDelta;
        private SerializedProperty targetProperty;
        private SerializedProperty fromCurrentProperty;
        private SerializedProperty fromProperty;
        private SerializedProperty toProperty;
        private SerializedProperty snappingProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            dOTweenSizeDelta = transition as DOTweenSizeDelta;
            targetProperty = serializedObject.FindProperty("target");
            fromCurrentProperty = serializedObject.FindProperty("fromCurrent");
            fromProperty = serializedObject.FindProperty("from");
            toProperty = serializedObject.FindProperty("to");
            snappingProperty = serializedObject.FindProperty("snapping");
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.PropertyField(targetProperty);

            EditorGUILayout.PropertyField(fromCurrentProperty);
            if (!dOTweenSizeDelta.FromCurrent)
            {
                GUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(fromProperty);
                if (GUILayout.Button("Set From", GUILayout.Width(100)))
                {
                    dOTweenSizeDelta.SetFromState();
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(toProperty);
            if (GUILayout.Button("Set To", GUILayout.Width(100)))
            {
                dOTweenSizeDelta.SetToState();
            }
            GUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(snappingProperty);

            if (GUI.changed)
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}

