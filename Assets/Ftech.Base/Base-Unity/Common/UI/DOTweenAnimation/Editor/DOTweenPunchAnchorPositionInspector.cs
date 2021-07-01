using UnityEditor;
using UnityEngine;

namespace Ftech.Lib.UI
{
    [CustomEditor(typeof(DOTweenPunchAnchorPosition))]
    public class DOTweenPunchAnchorPositionInspector : DOTweenTransitionInspector
    {
        private DOTweenPunchAnchorPosition dOTweenPunchAnchorPosition;
        private SerializedProperty targetProperty;
        private SerializedProperty fromCurrentProperty;
        private SerializedProperty fromProperty;
        private SerializedProperty punchProperty;
        private SerializedProperty vibratoProperty;
        private SerializedProperty elasticityProperty;
        private SerializedProperty snappingProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            dOTweenPunchAnchorPosition = transition as DOTweenPunchAnchorPosition;
            targetProperty = serializedObject.FindProperty("target");
            fromCurrentProperty = serializedObject.FindProperty("fromCurrent");
            fromProperty = serializedObject.FindProperty("from");
            punchProperty = serializedObject.FindProperty("punch");
            vibratoProperty = serializedObject.FindProperty("vibrato");
            elasticityProperty = serializedObject.FindProperty("elasticity");
            snappingProperty = serializedObject.FindProperty("snapping");
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();


            EditorGUILayout.PropertyField(targetProperty);

            EditorGUILayout.PropertyField(fromCurrentProperty);
            if (!dOTweenPunchAnchorPosition.FromCurrent)
            {
                GUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(fromProperty);
                if (GUILayout.Button("Set From", GUILayout.Width(100)))
                {
                    dOTweenPunchAnchorPosition.SetFromState();
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(punchProperty);
            if (GUILayout.Button("Set To", GUILayout.Width(100)))
            {
                dOTweenPunchAnchorPosition.SetToState();
            }
            GUILayout.EndHorizontal();


            EditorGUILayout.PropertyField(vibratoProperty);
            EditorGUILayout.PropertyField(elasticityProperty);
            EditorGUILayout.PropertyField(snappingProperty);

            if (GUI.changed)
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}