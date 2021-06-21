using UnityEditor;
using UnityEngine;

namespace AtoLib.UI
{
    [CustomEditor(typeof(DOTweenShakeScale))]
    public class DOTweenShakeScaleInspector : DOTweenTransitionInspector
    {
        private DOTweenShakeScale dOTweenShakeScale;

        protected override void OnEnable()
        {
            base.OnEnable();
            dOTweenShakeScale = transition as DOTweenShakeScale;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            dOTweenShakeScale.Target = (Transform)EditorGUILayout.ObjectField("Target", dOTweenShakeScale.Target, typeof(Transform), true);
            GUILayout.BeginHorizontal();
            dOTweenShakeScale.From = EditorGUILayout.Vector3Field("From", dOTweenShakeScale.From);
            if (GUILayout.Button("Set From", GUILayout.Width(80)))
            {
                dOTweenShakeScale.SetFromState();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            dOTweenShakeScale.Strength = EditorGUILayout.Vector3Field("Strength", dOTweenShakeScale.Strength);
            if (GUILayout.Button("Set Strength", GUILayout.Width(80)))
            {
                dOTweenShakeScale.SetToState();
            }
            GUILayout.EndHorizontal();
            dOTweenShakeScale.Vibrato = EditorGUILayout.IntField("Vibrato", dOTweenShakeScale.Vibrato);
            dOTweenShakeScale.Randomness = EditorGUILayout.FloatField("Randomness", dOTweenShakeScale.Randomness);
            dOTweenShakeScale.FadeOut = EditorGUILayout.Toggle("Fade Out", dOTweenShakeScale.FadeOut);
        }
    }
}
