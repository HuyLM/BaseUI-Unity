using UnityEditor;
using UnityEngine;

namespace AtoLib.UI
{
    [CustomEditor(typeof(DOTweenPunchScale))]
    public class DOTweenPunchScaleInspector : DOTweenTransitionInspector
    {
        private DOTweenPunchScale dOTweenPunchScale;

        protected override void OnEnable()
        {
            base.OnEnable();
            dOTweenPunchScale = transition as DOTweenPunchScale;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            dOTweenPunchScale.Target = (Transform)EditorGUILayout.ObjectField("Target", dOTweenPunchScale.Target, typeof(Transform), true);
            GUILayout.BeginHorizontal();
            dOTweenPunchScale.From = EditorGUILayout.Vector3Field("From", dOTweenPunchScale.From);
            if (GUILayout.Button("Set From", GUILayout.Width(80)))
            {
                dOTweenPunchScale.SetFromState();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            dOTweenPunchScale.Punch = EditorGUILayout.Vector3Field("Punch", dOTweenPunchScale.Punch);
            if (GUILayout.Button("Set Punch", GUILayout.Width(80)))
            {
                dOTweenPunchScale.SetToState();
            }
            GUILayout.EndHorizontal();
            dOTweenPunchScale.Vibrato = EditorGUILayout.IntField("Vibrato", dOTweenPunchScale.Vibrato);
            dOTweenPunchScale.Elasticity = EditorGUILayout.FloatField("Elasticity", dOTweenPunchScale.Elasticity);
        }
    }
}
