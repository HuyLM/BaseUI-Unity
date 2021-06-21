using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace AtoLib.UI
{
    [CustomEditor(typeof(DOTweenColor))]
    public class DOTweenColorInspector : DOTweenTransitionInspector
    {
        private DOTweenColor dOTweenColor;

        protected override void OnEnable()
        {
            base.OnEnable();
            dOTweenColor = transition as DOTweenColor;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            dOTweenColor.Target = (Graphic)EditorGUILayout.ObjectField("Target", dOTweenColor.Target, typeof(Graphic), true);
            GUILayout.BeginHorizontal();
            dOTweenColor.From = EditorGUILayout.ColorField("From", dOTweenColor.From);
            if (GUILayout.Button("Set From", GUILayout.Width(100)))
            {
                dOTweenColor.SetFromState();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            dOTweenColor.To = EditorGUILayout.ColorField("To", dOTweenColor.To);
            if (GUILayout.Button("Set To", GUILayout.Width(100)))
            {
                dOTweenColor.SetToState();
            }
            GUILayout.EndHorizontal();
        }
    }
}
