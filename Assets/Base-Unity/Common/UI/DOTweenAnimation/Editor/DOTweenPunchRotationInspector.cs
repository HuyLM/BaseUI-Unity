using UnityEditor;
using UnityEngine;

namespace AtoLib.UI
{
    [CustomEditor(typeof(DOTweenPunchRotation))]
    public class DOTweenPunchRotationInspector : DOTweenTransitionInspector
    {

        private DOTweenPunchRotation dOTweenPunchRotation;

        protected override void OnEnable()
        {
            base.OnEnable();
            dOTweenPunchRotation = transition as DOTweenPunchRotation;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            dOTweenPunchRotation.Target = (Transform)EditorGUILayout.ObjectField("Target", dOTweenPunchRotation.Target, typeof(Transform), true);
            GUILayout.BeginHorizontal();
            dOTweenPunchRotation.From = EditorGUILayout.Vector3Field("From", dOTweenPunchRotation.From);
            if (GUILayout.Button("Set From", GUILayout.Width(80)))
            {
                dOTweenPunchRotation.SetFromState();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            dOTweenPunchRotation.Punch = EditorGUILayout.Vector3Field("Punch", dOTweenPunchRotation.Punch);
            if (GUILayout.Button("Set Punch", GUILayout.Width(80)))
            {
                dOTweenPunchRotation.SetToState();
            }
            GUILayout.EndHorizontal();
            dOTweenPunchRotation.Vibrato = EditorGUILayout.IntField("Vibrato", dOTweenPunchRotation.Vibrato);
            dOTweenPunchRotation.Elasticity = EditorGUILayout.FloatField("Elasticity", dOTweenPunchRotation.Elasticity);
        }

    }
}
