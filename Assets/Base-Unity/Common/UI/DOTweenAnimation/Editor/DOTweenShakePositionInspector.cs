using UnityEditor;
using UnityEngine;

namespace AtoLib.UI
{
    [CustomEditor(typeof(DOTweenShakePosition))]
    public class DOTweenShakePositionInspector : DOTweenTransitionInspector
    {
        private DOTweenShakePosition dOTweenShakePosition;

        protected override void OnEnable()
        {
            base.OnEnable();
            dOTweenShakePosition = transition as DOTweenShakePosition;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            dOTweenShakePosition.Target = (Transform)EditorGUILayout.ObjectField("Target", dOTweenShakePosition.Target, typeof(Transform), true);
            GUILayout.BeginHorizontal();
            dOTweenShakePosition.From = EditorGUILayout.Vector3Field("From", dOTweenShakePosition.From);
            if (GUILayout.Button("Set From", GUILayout.Width(80)))
            {
                dOTweenShakePosition.SetFromState();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            dOTweenShakePosition.Strength = EditorGUILayout.Vector3Field("Strength", dOTweenShakePosition.Strength);
            if (GUILayout.Button("Set Strength", GUILayout.Width(80)))
            {
                dOTweenShakePosition.SetToState();
            }
            GUILayout.EndHorizontal();
            dOTweenShakePosition.Vibrato = EditorGUILayout.IntField("Vibrato", dOTweenShakePosition.Vibrato);
            dOTweenShakePosition.Randomness = EditorGUILayout.FloatField("Randomness", dOTweenShakePosition.Randomness);
            dOTweenShakePosition.FadeOut = EditorGUILayout.Toggle("Fade Out", dOTweenShakePosition.FadeOut);
        }
    }
}
