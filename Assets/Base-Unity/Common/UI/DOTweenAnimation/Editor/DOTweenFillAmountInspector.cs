using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace AtoLib.UI
{
    [CustomEditor(typeof(DOTweenFillAmount))]
    public class DOTweenFillAmountInspector : DOTweenTransitionInspector
    {
        private DOTweenFillAmount dOTweenFillAmount;

        protected override void OnEnable()
        {
            base.OnEnable();
            dOTweenFillAmount = transition as DOTweenFillAmount;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            dOTweenFillAmount.Target = (Image)EditorGUILayout.ObjectField("Target", dOTweenFillAmount.Target, typeof(Image), true);
            GUILayout.BeginHorizontal();
            dOTweenFillAmount.From = EditorGUILayout.Slider("From", dOTweenFillAmount.From, 0, 1);
            if (GUILayout.Button("Set From", GUILayout.Width(100)))
            {
                dOTweenFillAmount.SetFromState();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            dOTweenFillAmount.To = EditorGUILayout.Slider("To", dOTweenFillAmount.To, 0, 1);
            if (GUILayout.Button("Set To", GUILayout.Width(100)))
            {
                dOTweenFillAmount.SetToState();
            }
            GUILayout.EndHorizontal();
        }
    }
}
