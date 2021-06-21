using UnityEditor;
using UnityEngine;

namespace AtoLib.UI
{
    [CustomEditor(typeof(DOTweenScale))]
    public class DOTweenScaleInspector : DOTweenTransitionInspector
    {
        private DOTweenScale dOTweenScale;

        protected override void OnEnable()
        {
            base.OnEnable();
            dOTweenScale = transition as DOTweenScale;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            dOTweenScale.Target = (Transform)EditorGUILayout.ObjectField("Target", dOTweenScale.Target, typeof(Transform), true);
            GUILayout.BeginHorizontal();
            dOTweenScale.From = EditorGUILayout.Vector3Field("From", dOTweenScale.From);
            if (GUILayout.Button("Set From", GUILayout.Width(80)))
            {
                dOTweenScale.SetFromState();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            dOTweenScale.To = EditorGUILayout.Vector3Field("To", dOTweenScale.To);
            if (GUILayout.Button("Set To", GUILayout.Width(80)))
            {
                dOTweenScale.SetToState();
            }
            GUILayout.EndHorizontal();
        }
    }
}
