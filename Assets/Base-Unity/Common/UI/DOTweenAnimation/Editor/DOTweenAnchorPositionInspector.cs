using UnityEditor;
using UnityEngine;

namespace AtoLib.UI
{
    [CustomEditor(typeof(DOTweenAnchorPosition))]
    public class DOTweenAnchorPositionInspector : DOTweenTransitionInspector
    {
        private DOTweenAnchorPosition dOTweenAnchorPosition;

        protected override void OnEnable()
        {
            base.OnEnable();
            dOTweenAnchorPosition = transition as DOTweenAnchorPosition;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            dOTweenAnchorPosition.Target = (RectTransform)EditorGUILayout.ObjectField("Target", dOTweenAnchorPosition.Target, typeof(RectTransform), true);
            GUILayout.BeginHorizontal();
            dOTweenAnchorPosition.From = EditorGUILayout.Vector2Field("From", dOTweenAnchorPosition.From);
            if (GUILayout.Button("Set From", GUILayout.Width(80)))
            {
                dOTweenAnchorPosition.SetFromState();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            dOTweenAnchorPosition.To = EditorGUILayout.Vector2Field("To", dOTweenAnchorPosition.To);
            if (GUILayout.Button("Set To", GUILayout.Width(80)))
            {
                dOTweenAnchorPosition.SetToState();
            }
            GUILayout.EndHorizontal();
        }
    }
}
