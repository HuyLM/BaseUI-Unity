using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace Ftech.Lib.UI
{
    [CustomEditor(typeof(DOTweenText))]
    public class DOTweenTextInspector : DOTweenTransitionInspector
    {
        private DOTweenText dOTweenText;
        private SerializedProperty targetProperty;
        private SerializedProperty fromCurrentProperty;
        private SerializedProperty fromProperty;
        private SerializedProperty toProperty;
        private SerializedProperty richTextEnabledProperty;
        private SerializedProperty scrambleModeProperty;
        private SerializedProperty scrambleCharsProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            dOTweenText = transition as DOTweenText;
            targetProperty = serializedObject.FindProperty("target");
            fromCurrentProperty = serializedObject.FindProperty("fromCurrent");
            fromProperty = serializedObject.FindProperty("from");
            toProperty = serializedObject.FindProperty("to");
            richTextEnabledProperty = serializedObject.FindProperty("richTextEnabled");
            scrambleModeProperty = serializedObject.FindProperty("scrambleMode");
            scrambleCharsProperty = serializedObject.FindProperty("scrambleChars");

        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.PropertyField(targetProperty);

            EditorGUILayout.PropertyField(fromCurrentProperty);
            if (!dOTweenText.FromCurrent)
            {
                GUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(fromProperty);
                if (GUILayout.Button("Set From", GUILayout.Width(60)))
                {
                    dOTweenText.SetFromState();
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(toProperty);
            if (GUILayout.Button("Set To", GUILayout.Width(60)))
            {
                dOTweenText.SetToState();
            }
            GUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(richTextEnabledProperty);
            EditorGUILayout.PropertyField(scrambleModeProperty);
            if (dOTweenText.ScrambleMode == ScrambleMode.Custom)
            {
                EditorGUILayout.PropertyField(scrambleCharsProperty);
            }

            if (GUI.changed)
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}

