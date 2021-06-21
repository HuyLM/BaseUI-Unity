using UnityEditor;
using UnityEngine;

namespace AtoLib.UI
{
    [CustomEditor(typeof(DOTweenFade))]
    public class DOTweenFadeInspector : DOTweenTransitionInspector
    {
        private DOTweenFade dOTweenFade;

        protected override void OnEnable()
        {
            base.OnEnable();
            dOTweenFade = transition as DOTweenFade;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            dOTweenFade.Target = (CanvasGroup)EditorGUILayout.ObjectField("Target", dOTweenFade.Target, typeof(CanvasGroup), true);
            GUILayout.BeginHorizontal();
            dOTweenFade.From = EditorGUILayout.Slider("From", dOTweenFade.From, 0, 1);
            if (GUILayout.Button("Set From", GUILayout.Width(100)))
            {
                dOTweenFade.SetFromState();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            dOTweenFade.To = EditorGUILayout.Slider("To", dOTweenFade.To, 0, 1);
            if (GUILayout.Button("Set To", GUILayout.Width(100)))
            {
                dOTweenFade.SetToState();
            }
            GUILayout.EndHorizontal();
        }
    }
}
