using UnityEditor;
using UnityEngine;

namespace AtoLib.UI
{
    [CustomEditor(typeof(DOTweenSizeDelta))]
    public class DOTweenSizeDeltaInspector : DOTweenTransitionInspector
    {
        private DOTweenSizeDelta dOTweenSizeDelta;

        protected override void OnEnable()
        {
            base.OnEnable();
            dOTweenSizeDelta = transition as DOTweenSizeDelta;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            dOTweenSizeDelta.Target = (RectTransform)EditorGUILayout.ObjectField("Target", dOTweenSizeDelta.Target, typeof(RectTransform), true);
            GUILayout.BeginHorizontal();
            dOTweenSizeDelta.From = EditorGUILayout.Vector3Field("From", dOTweenSizeDelta.From);
            if (GUILayout.Button("Set From", GUILayout.Width(80)))
            {
                dOTweenSizeDelta.SetFromState();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            dOTweenSizeDelta.To = EditorGUILayout.Vector3Field("To", dOTweenSizeDelta.To);
            if (GUILayout.Button("Set To", GUILayout.Width(80)))
            {
                dOTweenSizeDelta.SetToState();
            }
            GUILayout.EndHorizontal();
            dOTweenSizeDelta.Snapping = EditorGUILayout.Toggle("Snapping", dOTweenSizeDelta.Snapping);
        }
    }
}

