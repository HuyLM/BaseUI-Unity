using Ftech.Lib.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Ftech.Lib.UI
{
    [CustomEditor(typeof(DOTweenFillAmount))]
    public class DOTweenFillAmountInspector : DOTweenTransitionInspector
    {
        private DOTweenFillAmount dOTweenFillAmount;
        private SerializedProperty targetProperty;
        private SerializedProperty fromCurrentProperty;
        private SerializedProperty fromProperty;
        private SerializedProperty toProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            dOTweenFillAmount = transition as DOTweenFillAmount;
            targetProperty = serializedObject.FindProperty("target");
            fromCurrentProperty = serializedObject.FindProperty("fromCurrent");
            fromProperty = serializedObject.FindProperty("from");
            toProperty = serializedObject.FindProperty("to");
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.PropertyField(targetProperty);

            EditorGUILayout.PropertyField(fromCurrentProperty);
            if (!dOTweenFillAmount.FromCurrent)
            {
                GUILayout.BeginHorizontal();
                EditorGUILayout.Slider(fromProperty, 0, 1);
                if (GUILayout.Button("Set From", GUILayout.Width(100)))
                {
                    dOTweenFillAmount.SetFromState();
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.BeginHorizontal();
            EditorGUILayout.Slider(toProperty, 0, 1);
            if (GUILayout.Button("Set To", GUILayout.Width(100)))
            {
                dOTweenFillAmount.SetToState();
            }
            GUILayout.EndHorizontal();

            if (GUI.changed)
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
