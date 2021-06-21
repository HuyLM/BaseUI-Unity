using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace AtoLib.UI
{
    [CustomEditor(typeof(DOTweenText))]
    public class DOTweenTextInspector : DOTweenTransitionInspector
    {
        private DOTweenText dOTweenText;

        protected override void OnEnable()
        {
            base.OnEnable();
            dOTweenText = transition as DOTweenText;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            dOTweenText.Target = (Text)EditorGUILayout.ObjectField("Target", dOTweenText.Target, typeof(Text), true);
            dOTweenText.From = EditorGUILayout.TextArea("From", dOTweenText.From);
            dOTweenText.To = EditorGUILayout.TextArea("To", dOTweenText.To);
            dOTweenText.RichTextEnabled = EditorGUILayout.Toggle("RichText Enabled", dOTweenText.RichTextEnabled);
            dOTweenText.ScrambleMode = (ScrambleMode)EditorGUILayout.EnumPopup("Scramble Mode", dOTweenText.ScrambleMode);
            dOTweenText.ScrambleChars = EditorGUILayout.TextField("Scramble Chars", dOTweenText.ScrambleChars);
        }
    }
}

