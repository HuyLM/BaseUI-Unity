using UnityEditor;
using UnityEngine;

namespace AtoLib.UI
{
    [CustomEditor(typeof(DOTweenShakeRotation))]
    public class DOTweenShakeRotationInspector : DOTweenTransitionInspector
    {
        private DOTweenShakeRotation dOTweenShakeRotation;

        protected override void OnEnable()
        {
            base.OnEnable();
            dOTweenShakeRotation = transition as DOTweenShakeRotation;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            dOTweenShakeRotation.Target = (Transform)EditorGUILayout.ObjectField("Target", dOTweenShakeRotation.Target, typeof(Transform), true);
            GUILayout.BeginHorizontal();
            dOTweenShakeRotation.From = EditorGUILayout.Vector3Field("From", dOTweenShakeRotation.From);
            if (GUILayout.Button("Set From", GUILayout.Width(80)))
            {
                dOTweenShakeRotation.SetFromState();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            dOTweenShakeRotation.Strength = EditorGUILayout.Vector3Field("Strength", dOTweenShakeRotation.Strength);
            if (GUILayout.Button("Set Strength", GUILayout.Width(80)))
            {
                dOTweenShakeRotation.SetToState();
            }
            GUILayout.EndHorizontal();
            dOTweenShakeRotation.Vibrato = EditorGUILayout.IntField("Vibrato", dOTweenShakeRotation.Vibrato);
            dOTweenShakeRotation.Randomness = EditorGUILayout.FloatField("Randomness", dOTweenShakeRotation.Randomness);
            dOTweenShakeRotation.FadeOut = EditorGUILayout.Toggle("Fade Out", dOTweenShakeRotation.FadeOut);
        }
    }
}
