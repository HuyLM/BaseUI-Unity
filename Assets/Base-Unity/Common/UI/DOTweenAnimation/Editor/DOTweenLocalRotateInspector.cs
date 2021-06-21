using UnityEditor;
using UnityEngine;

namespace AtoLib.UI
{
    [CustomEditor(typeof(DOTweenLocalRotate))]
    public class DOTweenLocalRotateInspector : DOTweenTransitionInspector
    {
        private DOTweenLocalRotate dOTweenLocalRotate;

        protected override void OnEnable()
        {
            base.OnEnable();
            dOTweenLocalRotate = transition as DOTweenLocalRotate;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            dOTweenLocalRotate.Target = (Transform)EditorGUILayout.ObjectField("Target", dOTweenLocalRotate.Target, typeof(Transform), true);
            GUILayout.BeginHorizontal();
            dOTweenLocalRotate.From = EditorGUILayout.Vector3Field("From", dOTweenLocalRotate.From);
            if (GUILayout.Button("Set From", GUILayout.Width(80)))
            {
                dOTweenLocalRotate.SetFromState();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            dOTweenLocalRotate.To = EditorGUILayout.Vector3Field("To", dOTweenLocalRotate.To);
            if (GUILayout.Button("Set To", GUILayout.Width(80)))
            {
                dOTweenLocalRotate.SetToState();
            }
            GUILayout.EndHorizontal();
        }
    }
}

