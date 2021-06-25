using UnityEditor;
using UnityEngine;

namespace Ftech.Lib.UI
{
    [CustomEditor(typeof(DOTweenShakeScale))]
    public class DOTweenShakeScaleInspector : DOTweenTransitionInspector
    {
        private DOTweenShakeScale dOTweenShakeScale;
        private SerializedProperty targetProperty;
        private SerializedProperty fromCurrentProperty;
        private SerializedProperty fromProperty;
        private SerializedProperty strengthProperty;
        private SerializedProperty vibratoProperty;
        private SerializedProperty randomnessProperty;
        private SerializedProperty fadeOutProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            dOTweenShakeScale = transition as DOTweenShakeScale;
            targetProperty = serializedObject.FindProperty("target");
            fromCurrentProperty = serializedObject.FindProperty("fromCurrent");
            fromProperty = serializedObject.FindProperty("from");
            strengthProperty = serializedObject.FindProperty("strength");
            vibratoProperty = serializedObject.FindProperty("vibrato");
            randomnessProperty = serializedObject.FindProperty("randomness");
            fadeOutProperty = serializedObject.FindProperty("fadeOut");
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.PropertyField(fromCurrentProperty);
            if (!dOTweenShakeScale.FromCurrent)
            {
                GUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(fromProperty);
                if (GUILayout.Button("Set From", GUILayout.Width(100)))
                {
                    dOTweenShakeScale.SetFromState();
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(strengthProperty);
            GUILayout.EndHorizontal();


            EditorGUILayout.PropertyField(vibratoProperty);
            EditorGUILayout.PropertyField(randomnessProperty);
            EditorGUILayout.PropertyField(fadeOutProperty);

            if (GUI.changed)
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
