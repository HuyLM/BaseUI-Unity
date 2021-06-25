using Ftech.Lib.UI;
using UnityEditor;
using UnityEngine;

namespace Ftech.Lib.UI
{
    [CustomEditor(typeof(DOTweenPunchRotation))]
    public class DOTweenPunchRotationInspector : DOTweenTransitionInspector
    {

        private DOTweenPunchRotation dOTweenPunchRotation;
        private SerializedProperty targetProperty;
        private SerializedProperty fromCurrentProperty;
        private SerializedProperty fromProperty;
        private SerializedProperty toProperty;
        private SerializedProperty vibratoProperty;
        private SerializedProperty elasticityProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            dOTweenPunchRotation = transition as DOTweenPunchRotation;
            targetProperty = serializedObject.FindProperty("target");
            fromCurrentProperty = serializedObject.FindProperty("fromCurrent");
            fromProperty = serializedObject.FindProperty("from");
            toProperty = serializedObject.FindProperty("to");
            vibratoProperty = serializedObject.FindProperty("vibrato");
            elasticityProperty = serializedObject.FindProperty("elasticity");
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.PropertyField(targetProperty);

            EditorGUILayout.PropertyField(fromCurrentProperty);
            if (!dOTweenPunchRotation.FromCurrent)
            {
                GUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(fromProperty);
                if (GUILayout.Button("Set From", GUILayout.Width(100)))
                {
                    dOTweenPunchRotation.SetFromState();
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(toProperty);
            if (GUILayout.Button("Set To", GUILayout.Width(100)))
            {
                dOTweenPunchRotation.SetToState();
            }
            GUILayout.EndHorizontal();


            EditorGUILayout.PropertyField(vibratoProperty);
            EditorGUILayout.PropertyField(elasticityProperty);

            if (GUI.changed)
            {
                serializedObject.ApplyModifiedProperties();
            }
        }

    }
}
