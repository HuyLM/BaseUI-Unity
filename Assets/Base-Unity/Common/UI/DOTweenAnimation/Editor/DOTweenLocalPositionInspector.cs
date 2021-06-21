using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AtoLib.UI
{
    [CustomEditor(typeof(DOTweenLocalPosition))]
    public class DOTweenLocalPositionInspector : DOTweenTransitionInspector
    {
        private DOTweenLocalPosition dOTweenLocalPosition;

        protected override void OnEnable()
        {
            base.OnEnable();
            dOTweenLocalPosition = transition as DOTweenLocalPosition;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            dOTweenLocalPosition.Target = (Transform)EditorGUILayout.ObjectField("Target", dOTweenLocalPosition.Target, typeof(Transform), true);
            GUILayout.BeginHorizontal();
            dOTweenLocalPosition.From = EditorGUILayout.Vector3Field("From", dOTweenLocalPosition.From);
            if (GUILayout.Button("Set From", GUILayout.Width(80)))
            {
                dOTweenLocalPosition.SetFromState();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            dOTweenLocalPosition.To = EditorGUILayout.Vector3Field("To", dOTweenLocalPosition.To);
            if (GUILayout.Button("Set To", GUILayout.Width(80)))
            {
                dOTweenLocalPosition.SetToState();
            }
            GUILayout.EndHorizontal();
        }
    }
}
