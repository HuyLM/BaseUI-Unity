using UnityEditor;
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
            dOTweenAlpha.From = EditorGUILayout.FloatField("From", dOTweenAlpha.From);
            dOTweenAlpha.To = EditorGUILayout.FloatField("To", dOTweenAlpha.To);
        }
    }
}

