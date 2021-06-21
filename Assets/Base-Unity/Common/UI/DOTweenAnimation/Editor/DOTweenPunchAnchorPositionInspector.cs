using UnityEditor;
using UnityEngine;

namespace AtoLib.UI
{
    [CustomEditor(typeof(DOTweenPunchAnchorPosition))]
    public class DOTweenPunchAnchorPositionInspector : DOTweenTransitionInspector
    {
        private DOTweenPunchAnchorPosition dOTweenPunchAnchorPosition;

        protected override void OnEnable()
        {
            base.OnEnable();
            dOTweenPunchAnchorPosition = transition as DOTweenPunchAnchorPosition;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            dOTweenPunchAnchorPosition.Target = (RectTransform)EditorGUILayout.ObjectField("Target", dOTweenPunchAnchorPosition.Target, typeof(RectTransform), true);
            GUILayout.BeginHorizontal();
            dOTweenPunchAnchorPosition.From = EditorGUILayout.Vector2Field("From", dOTweenPunchAnchorPosition.From);
            if (GUILayout.Button("Set From", GUILayout.Width(80)))
            {
                dOTweenPunchAnchorPosition.SetFromState();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            dOTweenPunchAnchorPosition.Punch = EditorGUILayout.Vector2Field("Punch", dOTweenPunchAnchorPosition.Punch);
            if (GUILayout.Button("Set Punch", GUILayout.Width(80)))
            {
                dOTweenPunchAnchorPosition.SetToState();
            }
            GUILayout.EndHorizontal();
            dOTweenPunchAnchorPosition.Vibrato = EditorGUILayout.IntField("Vibrato", dOTweenPunchAnchorPosition.Vibrato);
            dOTweenPunchAnchorPosition.Elasticity = EditorGUILayout.FloatField("Elasticity", dOTweenPunchAnchorPosition.Elasticity);
            dOTweenPunchAnchorPosition.Snapping = EditorGUILayout.Toggle("Snapping", dOTweenPunchAnchorPosition.Snapping);
        }
    }
}