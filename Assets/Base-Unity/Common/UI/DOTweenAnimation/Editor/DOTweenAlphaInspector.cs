using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace AtoLib.UI
{
    [CustomEditor(typeof(DOTweenAlpha))]
    public class DOTweenAlphaInspector : DOTweenTransitionInspector
    {
        private DOTweenAlpha dOTweenAlpha;

        protected override void OnEnable()
        {
            base.OnEnable();
            dOTweenAlpha = transition as DOTweenAlpha;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            dOTweenAlpha.Target = (Graphic)EditorGUILayout.ObjectField("Target", dOTweenAlpha.Target, typeof(Graphic), true);
            GUILayout.BeginHorizontal();
            dOTweenAlpha.From = EditorGUILayout.Slider("From", dOTweenAlpha.From, 0, 1);
            if (GUILayout.Button("Set From", GUILayout.Width(100)))
            {
                dOTweenAlpha.SetFromState();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            dOTweenAlpha.To = EditorGUILayout.Slider("To", dOTweenAlpha.To, 0, 1);
            if (GUILayout.Button("Set To", GUILayout.Width(100)))
            {
                dOTweenAlpha.SetToState();
            }
            GUILayout.EndHorizontal();
        }
    }
}

